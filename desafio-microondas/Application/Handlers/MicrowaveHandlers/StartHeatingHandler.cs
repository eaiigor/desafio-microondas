using AutoMapper;
using desafio_microondas.API.DTOs;
using desafio_microondas.Application.Commands.MicrowaveCommands;
using desafio_microondas.Domain.Entities;
using desafio_microondas.Domain.Enums;
using desafio_microondas.Infrastructure.Repositories.HeatingProgramRepository;
using desafio_microondas.Infrastructure.Repositories.MicrowaveRepository;
using desafio_microondas.Infrastructure.Services;
using desafio_microondas.Infrastructure.Services.MicrowaveService;
using MediatR;

namespace desafio_microondas.Application.Handlers.MicrowaveHandlers;

public class StartHeatingHandler : IRequestHandler<StartHeatingCommand, MicrowaveDTO>
{
    private readonly IMicrowaveService _microwaveService;
    private readonly IMicrowaveRepository _microwaveRepository;
    private readonly IHeatingProgramRepository _heatingProgramRepository;

    public StartHeatingHandler(IMicrowaveRepository microwaveRepository, IMicrowaveService microwaveService, IHeatingProgramRepository heatingProgramRepository)
    {
        _microwaveService = microwaveService;
        _heatingProgramRepository = heatingProgramRepository;
        _microwaveRepository = microwaveRepository;
    }

    public async Task<MicrowaveDTO> Handle(StartHeatingCommand request, CancellationToken cancellationToken)
    {
        var microwave = await _microwaveRepository.GetMicrowaveAsync(1);
        var heatingProgram = await _heatingProgramRepository.GetHeatingProgramAsync(request.HeatingProgramId ?? -10);

        if (heatingProgram == null) return await _microwaveService.StartHeating(microwave, request.Time, request.Power);
        return await _microwaveService.StartHeating(microwave, heatingProgram.Time, heatingProgram.Power, true);

    }
}