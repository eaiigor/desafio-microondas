using MediatR;

namespace desafio_microondas.API.DTOs;

public class HeatingProgramDTO
{
    public long Id { get; set; }
    public string Time { get; set; }
    public long Power { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Instructions { get; set; }
}