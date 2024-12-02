using FluentValidation.Results;

namespace desafio_microondas.Domain.Interfaces
{
    public interface IMicrowaveCommand
    {
        public ValidationResult Validate();
    }
}