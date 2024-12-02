namespace desafio_microondas.Domain.Entities;

public class HeatingProgram
{
    public HeatingProgram()
    {
    }

    public HeatingProgram(long id, string name, long time, long power, string? instructions = "")
    {
        Id = id;
        Name = name;
        Time = time;
        Power = power;
        Instructions = instructions;
    }

    public string FormattedTime
    {
        get
        {
            var minutes = Time / 60;
            var seconds = Time % 60;

            return $"{minutes:D1}:{seconds:D2}";
        }
    }

    public long Id { get; set; }
    public long Time { get; set; }
    public long Power { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Instructions { get; set; }
    public long? MicrowaveId { get; set; }
    public Microwave? Microwave { get; set; }
}