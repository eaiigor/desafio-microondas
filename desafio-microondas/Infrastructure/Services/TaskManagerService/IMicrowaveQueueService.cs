using desafio_microondas.Domain.Entities;

namespace desafio_microondas.Infrastructure.Services.TaskManagerService;

public interface IMicrowaveQueueService
{
    void EnqueueMicrowave(Microwave microwave);
    bool TryDequeueMicrowave(out Microwave microwave);
    
    
}