using desafio_microondas.Domain.Entities;

namespace desafio_microondas.Infrastructure.Repositories.HeatingProgramRepository;

public interface IHeatingProgramRepository
{
    Task<HeatingProgram?> GetHeatingProgramAsync(long id);
    Task<List<HeatingProgram>> GetHeatingProgramsAsync();
    Task SaveHeatingProgramAsync(HeatingProgram heatingProgram);
    Task DeleteHeatingProgramAsync(HeatingProgram heatingProgram);

    Task CreateHeatingProgramAsync(HeatingProgram heatingProgram);
}