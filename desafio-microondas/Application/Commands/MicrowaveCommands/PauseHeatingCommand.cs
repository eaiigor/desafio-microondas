using desafio_microondas.API.DTOs;
using desafio_microondas.Domain.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace desafio_microondas.Application.Commands.MicrowaveCommands;

public class PauseHeatingCommand : IMicrowaveCommand, IRequest<MicrowaveDTO>
{
    private class PauseHeatingCommandValidator : AbstractValidator<PauseHeatingCommand>
    {
        public PauseHeatingCommandValidator()
        {
        }
    }

    public ValidationResult Validate() => new PauseHeatingCommandValidator().Validate(this);
}