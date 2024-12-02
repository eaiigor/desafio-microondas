using desafio_microondas.API.DTOs;
using desafio_microondas.Domain.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace desafio_microondas.Application.Commands.HeatingProgramCommands;

public class CreateHeatingProgramCommand : IMicrowaveCommand, IRequest<HeatingProgramDTO>
{
    public long Time { get; set; }
    public string Name { get; set; }
    public long Power { get; set; }
    public string? Instructions { get; set; }
    public long? MicrowaveId { get; set; }

    private class CreateHeatingProgramCommandValidator : AbstractValidator<CreateHeatingProgramCommand>
    {
        public CreateHeatingProgramCommandValidator()
        {
            RuleFor(x => x.Time).GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Power).GreaterThan(0).LessThanOrEqualTo(10);
        }
    }

    public ValidationResult Validate() => new CreateHeatingProgramCommandValidator().Validate(this);
}