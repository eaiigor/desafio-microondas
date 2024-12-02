using desafio_microondas.API.DTOs;

namespace desafio_microondas.Application.Queries.HeatingProgramQueries;

public interface IHeatingProgramQueries
{
    Task<List<HeatingProgramDTO>> GetHeatingProgramsAsync();
    Task<HeatingProgramDTO?> GetHeatingProgramAsync(long id);
}