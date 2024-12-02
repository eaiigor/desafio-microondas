using desafio_microondas.API.DTOs;
using desafio_microondas.Application.Commands.MicrowaveCommands;
using desafio_microondas.Infrastructure.Repositories.MicrowaveRepository;
using desafio_microondas.Infrastructure.Services;
using desafio_microondas.Infrastructure.Services.MicrowaveService;
using MediatR;

namespace desafio_microondas.Application.Handlers.MicrowaveHandlers;

public class StopHeatingHandler : IRequestHandler<StopHeatingCommand, MicrowaveDTO>
{
    private readonly IMicrowaveService _microwaveService;
    private readonly IMicrowaveRepository _microwaveRepository;

    public StopHeatingHandler(IMicrowaveService microwaveService, IMicrowaveRepository microwaveRepository)
    {
        _microwaveService = microwaveService;
        _microwaveRepository = microwaveRepository;
    }

    public async Task<MicrowaveDTO> Handle(StopHeatingCommand request, CancellationToken cancellationToken)
    {
        var microwave = await _microwaveRepository.GetMicrowaveAsync(1);
        return await _microwaveService.StopHeating(microwave);
    }
}