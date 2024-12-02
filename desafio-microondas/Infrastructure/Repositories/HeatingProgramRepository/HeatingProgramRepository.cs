using desafio_microondas.Domain.Entities;
using desafio_microondas.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace desafio_microondas.Infrastructure.Repositories.HeatingProgramRepository;

public class HeatingProgramRepository : IHeatingProgramRepository
{
    private readonly MicrowaveDbContext _ctx;

    private List<HeatingProgram> _defaultHeatingPrograms = new()
    {
        new HeatingProgram(id: -4, name: "Pipoca", time: 60 * 3, power: 7, instructions: "Observar o barulho de estouros do milho, caso houver um intervalo de mais de 10 segundos entre um\nestouro e outro, interrompa o aquecimento"),
        new HeatingProgram(id: -3, name: "Leite", time: 60 * 5, power: 5, instructions: "Cuidado com aquecimento de líquidos, o choque térmico aliado ao movimento do recipiente pode\ncausar fervura imediata causando risco de queimaduras."),
        new HeatingProgram(id: -2, name: "Carne", time: 60 * 14, power: 4, instructions: "Interrompa o processo na metade e vire o conteúdo com a parte de baixo para cima para o\ndescongelamento uniforme."),
        new HeatingProgram(id: -1, name: "Frango", time: 60 * 8, power: 7, instructions: "Interrompa o processo na metade e vire o conteúdo com a parte de baixo para cima para o\ndescongelamento uniforme."),
        new HeatingProgram(id: 0, name: "Feijão", time: 60 * 8, power: 9, instructions: "Deixe o recipiente destampado e em casos de plástico, cuidado ao retirar o recipiente pois o mesmo\npode perder resistência em altas temperaturas."),
    };

    public HeatingProgramRepository(MicrowaveDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<HeatingProgram?> GetHeatingProgramAsync(long id)
    {
        var heatingProgram = _defaultHeatingPrograms.FirstOrDefault(x => x.Id == id) ??
                             await _ctx.HeatingProgram.FirstOrDefaultAsync(x => x.Id == id);
        return heatingProgram;
    }


    public async Task<List<HeatingProgram>> GetHeatingProgramsAsync()
    {
        var heatingPrograms = await _ctx.HeatingProgram.ToListAsync();
        return _defaultHeatingPrograms.Concat(heatingPrograms).ToList();
    }

    public Task SaveHeatingProgramAsync(HeatingProgram heatingProgram)
    {
        _ctx.Update(heatingProgram);
        return _ctx.SaveChangesAsync();
    }

    public Task DeleteHeatingProgramAsync(HeatingProgram heatingProgram)
    {
        _ctx.HeatingProgram.Remove(heatingProgram);
        return _ctx.SaveChangesAsync();
    }

    public async Task CreateHeatingProgramAsync(HeatingProgram heatingProgram)
    {
        await _ctx.HeatingProgram.AddAsync(heatingProgram);
        await _ctx.SaveChangesAsync();
    }
}