using System.Collections.Concurrent;
using AutoMapper;
using desafio_microondas.API.DTOs;
using desafio_microondas.Domain.Entities;
using desafio_microondas.Domain.Enums;
using desafio_microondas.Infrastructure.Hubs;
using desafio_microondas.Infrastructure.Repositories.MicrowaveRepository;
using desafio_microondas.Infrastructure.Services.TaskManagerService;
using Microsoft.AspNetCore.SignalR;

namespace desafio_microondas.Infrastructure.Services.MicrowaveService;

public class MicrowaveService : IMicrowaveService
{
    private readonly IMapper _mapper;
    private readonly IMicrowaveRepository _microwaveRepository;
    private readonly IMicrowaveQueueService _microwaveQueueService;
    private readonly IHubContext<MicrowaveHub> _hubContext;

    public MicrowaveService(IMapper mapper,
        IMicrowaveRepository microwaveRepository,
        IMicrowaveQueueService microwaveQueueService,
        IHubContext<MicrowaveHub> hubContext)
    {
        _mapper = mapper;
        _microwaveRepository = microwaveRepository;
        _microwaveQueueService = microwaveQueueService;
        _hubContext = hubContext;
    }

    private static readonly ConcurrentDictionary<long, Microwave> Microwaves = new();

    public async Task<MicrowaveDTO> StartHeating(Microwave microwave, long time, long power,
        bool? isHeatingProgram = null)
    {
        var microwaveInstance = Microwaves.GetOrAdd(microwave.Id, microwave);
        if (isHeatingProgram != null)
        {
            microwaveInstance.IsHeatingProgram = isHeatingProgram.Value;
        }
        switch (microwaveInstance.State)
        {
            case EMicrowaveState.Idle:
                microwaveInstance.StartHeating(time, power);
                break;
            case EMicrowaveState.Heating:
                microwaveInstance.AddTime(microwave.IsHeatingProgram ? 0 : 30);
                break;
            case EMicrowaveState.Paused:
                microwaveInstance.ResumeHeating();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        Microwaves.AddOrUpdate(microwave.Id, microwaveInstance, (key, value) => microwaveInstance);
        await SaveMicrowave(microwaveInstance);
        _microwaveQueueService.EnqueueMicrowave(microwaveInstance);
        await SendMicrowaveState(microwaveInstance);
        return _mapper.Map<MicrowaveDTO>(microwaveInstance);
    }

    public async Task<MicrowaveDTO> StopHeating(Microwave microwave)
    {
        var microwaveInstance = Microwaves.GetOrAdd(microwave.Id, microwave);
        microwaveInstance.StopHeating();
        microwaveInstance.IsHeatingProgram = false;
        await SaveMicrowave(microwaveInstance);

        Microwaves.AddOrUpdate(microwave.Id, microwaveInstance, (key, value) => microwaveInstance);
        await SendMicrowaveState(microwaveInstance);
        return _mapper.Map<MicrowaveDTO>(microwaveInstance);
    }

    public async Task<MicrowaveDTO> UpdateTime(Microwave microwave, long time)
    {
        var microwaveInstance = Microwaves.GetOrAdd(microwave.Id, microwave);
        microwaveInstance.UpdateTime(time);
        Microwaves.AddOrUpdate(microwave.Id, microwaveInstance, (key, value) => microwaveInstance);
        await HeatingProgress(microwaveInstance);
        return _mapper.Map<MicrowaveDTO>(microwaveInstance);
    }

    public async Task<MicrowaveDTO> PauseHeating(Microwave microwave)
    {
        var microwaveInstance = Microwaves.GetOrAdd(microwave.Id, microwave);
        microwaveInstance.PauseHeating();
        await SaveMicrowave(microwaveInstance);
        Microwaves.AddOrUpdate(microwave.Id, microwaveInstance, (key, value) => microwaveInstance);
        await SendMicrowaveState(microwaveInstance);
        return _mapper.Map<MicrowaveDTO>(microwaveInstance);
    }

    public Task SaveMicrowave(Microwave microwave) => _microwaveRepository.SaveMicrowaveAsync(microwave);

    public bool IsHeating(long id) =>
        Microwaves.TryGetValue(id, out var microwave) && microwave.State == EMicrowaveState.Heating;


    private async Task SendMicrowaveState(Microwave microwave)
    {
        await _hubContext.Clients.Group(microwave.Id.ToString()).SendAsync("MicrowaveState", microwave.State);
    }

    private async Task HeatingProgress(Microwave microwave)
    {
        var dto = _mapper.Map<MicrowaveDTO>(microwave);
        await _hubContext.Clients.Group(microwave.Id.ToString()).SendAsync("HeatingProgress", dto);
    }
}