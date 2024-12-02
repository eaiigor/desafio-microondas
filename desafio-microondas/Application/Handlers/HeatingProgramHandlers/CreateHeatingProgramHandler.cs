using AutoMapper;
using desafio_microondas.API.DTOs;
using desafio_microondas.Application.Commands.HeatingProgramCommands;
using desafio_microondas.Domain.Entities;
using desafio_microondas.Infrastructure.Repositories.HeatingProgramRepository;
using MediatR;

namespace desafio_microondas.Application.Handlers.HeatingProgramHandlers;

public class CreateHeatingProgramHandler : IRequestHandler<CreateHeatingProgramCommand, HeatingProgramDTO>
{
    private readonly IHeatingProgramRepository _heatingProgramRepository;
    private readonly IMapper _mapper;

    public CreateHeatingProgramHandler(IHeatingProgramRepository heatingProgramRepository, IMapper mapper)
    {
        _heatingProgramRepository = heatingProgramRepository;
        _mapper = mapper;
    }

    public async Task<HeatingProgramDTO> Handle(CreateHeatingProgramCommand request,
        CancellationToken cancellationToken)
    {
        var heatingProgram = _mapper.Map<HeatingProgram>(request);
        await _heatingProgramRepository.CreateHeatingProgramAsync(heatingProgram);

        return _mapper.Map<HeatingProgramDTO>(heatingProgram);
    }
}