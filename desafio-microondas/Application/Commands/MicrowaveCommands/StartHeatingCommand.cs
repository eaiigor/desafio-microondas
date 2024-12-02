using desafio_microondas.API.DTOs;
using desafio_microondas.Domain.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace desafio_microondas.Application.Commands.MicrowaveCommands;

public class StartHeatingCommand : IRequest<MicrowaveDTO>, IMicrowaveCommand
{
    public long Time { get; set; }
    public long Power { get; set; }
    
    public long? HeatingProgramId { get; set; }
    private class StartHeatingCommandValidator : AbstractValidator<StartHeatingCommand>
    {
        public StartHeatingCommandValidator()
        {
            RuleFor(x => x.Time).GreaterThan(0);
            RuleFor(x => x.Power).GreaterThan(0);
        }
    }

    public ValidationResult Validate() => new StartHeatingCommandValidator().Validate(this);
}