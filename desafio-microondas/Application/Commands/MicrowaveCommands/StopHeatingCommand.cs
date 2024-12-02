using desafio_microondas.API.DTOs;
using desafio_microondas.Domain.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace desafio_microondas.Application.Commands.MicrowaveCommands;

public class StopHeatingCommand : IRequest<MicrowaveDTO>, IMicrowaveCommand
{
    private class StopHeatingCommandValidator : AbstractValidator<StopHeatingCommand>
    {
        public StopHeatingCommandValidator()
        {
        }
    }

    public ValidationResult Validate() => new StopHeatingCommandValidator().Validate(this);
}