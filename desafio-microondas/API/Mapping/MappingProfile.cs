using AutoMapper;
using desafio_microondas.API.DTOs;
using desafio_microondas.Application.Commands.HeatingProgramCommands;
using desafio_microondas.Domain.Entities;

namespace desafio_microondas.API.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Microwave, MicrowaveDTO>()
            .ForMember(m => m.RemainingTime, opt => opt.MapFrom(m => m.FormattedTime));
        
        CreateMap<HeatingProgram, HeatingProgramDTO>()
            .ForMember(h => h.Time, opt => opt.MapFrom(h => h.FormattedTime));
        
        CreateMap<CreateHeatingProgramCommand, HeatingProgram>();
    }
}