namespace desafio_microondas.Domain.Models;

public class MicrowaveTask
{
    public MicrowaveTask(long id, Task task)
    {
        Id = id;
        Task = task;
    }

    public long Id { get; set; }
    public Task Task { get; set; }
}