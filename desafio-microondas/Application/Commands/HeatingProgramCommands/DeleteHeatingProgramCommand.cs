using Bogus;
using desafio_microondas.API.DTOs;
using desafio_microondas.Domain.Interfaces;
using FluentValidation;
using MediatR;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace desafio_microondas.Application.Commands.HeatingProgramCommands;

public class DeleteHeatingProgramCommand : IRequest<HeatingProgramDTO>, IMicrowaveCommand
{
    public long Id { get; set; }

    private class DeleteHeatingProgramCommandValidator : AbstractValidator<DeleteHeatingProgramCommand>
    {
        public DeleteHeatingProgramCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }

    public ValidationResult Validate() => new DeleteHeatingProgramCommandValidator().Validate(this);
}