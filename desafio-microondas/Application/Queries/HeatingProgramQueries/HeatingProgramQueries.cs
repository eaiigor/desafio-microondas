using AutoMapper;
using desafio_microondas.API.DTOs;
using desafio_microondas.Infrastructure.Repositories.HeatingProgramRepository;

namespace desafio_microondas.Application.Queries.HeatingProgramQueries;

public class HeatingProgramQueries : IHeatingProgramQueries
{
    private readonly IHeatingProgramRepository _heatingProgramRepository;
    private readonly IMapper _mapper;

    public HeatingProgramQueries(IHeatingProgramRepository heatingProgramRepository, IMapper mapper)
    {
        _heatingProgramRepository = heatingProgramRepository;
        _mapper = mapper;
    }

    public async Task<List<HeatingProgramDTO>> GetHeatingProgramsAsync()
    {
        var heatingPrograms = await _heatingProgramRepository.GetHeatingProgramsAsync();
        return _mapper.Map<List<HeatingProgramDTO>>(heatingPrograms);
    }

    public async Task<HeatingProgramDTO?> GetHeatingProgramAsync(long id)
    {
        var heatingProgram = await _heatingProgramRepository.GetHeatingProgramAsync(id);
        return _mapper.Map<HeatingProgramDTO?>(heatingProgram);
    }
}