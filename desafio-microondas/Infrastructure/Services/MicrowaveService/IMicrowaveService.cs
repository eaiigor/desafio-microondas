using desafio_microondas.API.DTOs;
using desafio_microondas.Domain.Entities;

namespace desafio_microondas.Infrastructure.Services.MicrowaveService;

public interface IMicrowaveService
{
    Task<MicrowaveDTO> StartHeating(Microwave microwave, long time, long power, bool? isHeatingProgram = null);
    Task<MicrowaveDTO> StopHeating(Microwave microwave);

    Task<MicrowaveDTO> PauseHeating(Microwave microwave);

    Task<MicrowaveDTO> UpdateTime(Microwave microwave, long time);
    Task SaveMicrowave(Microwave microwave);

    bool IsHeating(long id);
}