using desafio_microondas.Domain.Enums;

namespace desafio_microondas.Domain.Entities;

public class Microwave
{
    public long Id { get; set; }
    public EMicrowaveState State { get; set; }
    public long Time { get; set; }
    public long Power { get; set; }
    public HashSet<HeatingProgram> HeatingPrograms { get; set; } = new();
    public bool IsHeatingProgram { get; set; }

    public string FormattedTime
    {
        get
        {
            var minutes = Time / 60;
            var seconds = Time % 60;

            return $"{minutes:D1}:{seconds:D2}";
        }
    }

    public void StartHeating(long time, long power)
    {
        Time = time;
        Power = power;
        State = EMicrowaveState.Heating;
    }

    public void StopHeating()
    {
        Time = 0;
        Power = 0;
        State = EMicrowaveState.Idle;
    }

    public void PauseHeating() => State = EMicrowaveState.Paused;

    public void ResumeHeating() => State = EMicrowaveState.Heating;

    public void AddTime(long time) => Time += time;
    public void UpdateTime(long time) => Time = time;
}