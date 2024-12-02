using desafio_microondas.Domain.Entities;
using desafio_microondas.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace desafio_microondas.Infrastructure.Repositories.MicrowaveRepository;

public class MicrowaveRepository : IMicrowaveRepository
{
    private readonly MicrowaveDbContext _ctx;

    public MicrowaveRepository(MicrowaveDbContext ctx)
    {
        _ctx = ctx;
    }

    public Task<Microwave> GetMicrowaveAsync(long id) => _ctx.Microwave.AsNoTracking().FirstAsync(x => x.Id == id);

    public async Task SaveMicrowaveAsync(Microwave microwave)
    {
        var existingMicrowave = await _ctx.Microwave.FirstAsync(x => x.Id == microwave.Id);

        existingMicrowave.Time = microwave.Time;
        existingMicrowave.Power = microwave.Power;
        existingMicrowave.State = microwave.State;
        existingMicrowave.IsHeatingProgram = microwave.IsHeatingProgram;

        await _ctx.SaveChangesAsync();
    }
}