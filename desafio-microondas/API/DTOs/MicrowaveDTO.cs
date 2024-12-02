using desafio_microondas.Domain.Enums;

namespace desafio_microondas.API.DTOs;

public class MicrowaveDTO
{
    public string RemainingTime { get; set; }
    public long Power { get; set; }
    public EMicrowaveState State { get; set; }
    public bool IsHeatingProgram { get; set; }
}