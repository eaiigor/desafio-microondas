using AutoMapper;
using desafio_microondas.API.DTOs;
using desafio_microondas.Application.Commands.HeatingProgramCommands;
using desafio_microondas.Infrastructure.Repositories.HeatingProgramRepository;
using MediatR;

namespace desafio_microondas.Application.Handlers.HeatingProgramHandlers;

public class DeleteHeatingProgramHandler : IRequestHandler<DeleteHeatingProgramCommand, HeatingProgramDTO>
{
    private readonly IMapper _mapper;
    private readonly IHeatingProgramRepository _heatingProgramRepository;

    public DeleteHeatingProgramHandler(IMapper mapper, IHeatingProgramRepository heatingProgramRepository)
    {
        _mapper = mapper;
        _heatingProgramRepository = heatingProgramRepository;
    }

    public async Task<HeatingProgramDTO> Handle(DeleteHeatingProgramCommand request,
        CancellationToken cancellationToken)
    {
        var heatingProgram = await _heatingProgramRepository.GetHeatingProgramAsync(request.Id);
        if (heatingProgram == null) return new();
        await _heatingProgramRepository.DeleteHeatingProgramAsync(heatingProgram);
        return _mapper.Map<HeatingProgramDTO>(heatingProgram);
    }
}