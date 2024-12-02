using desafio_microondas.Domain.Entities;

namespace desafio_microondas.Infrastructure.Repositories.MicrowaveRepository;

public interface IMicrowaveRepository
{
    public Task<Microwave> GetMicrowaveAsync(long id);
    
    public Task SaveMicrowaveAsync(Microwave microwave);
}