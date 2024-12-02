using System.Collections.Concurrent;
using desafio_microondas.Domain.Entities;

namespace desafio_microondas.Infrastructure.Services.TaskManagerService;

public class MicrowaveQueueService : IMicrowaveQueueService
{
    private readonly ConcurrentQueue<Microwave> _microwaveQueue = new();

    public void EnqueueMicrowave(Microwave microwave)
    {
        if (IsInQueue(microwave.Id)) return;
        _microwaveQueue.Enqueue(microwave);
    }

    public bool TryDequeueMicrowave(out Microwave microwave) => _microwaveQueue.TryDequeue(out microwave);
    private bool IsInQueue(long microwaveId) => _microwaveQueue.Any(m => m.Id == microwaveId);
}