using FluentValidation.Results;

namespace TalkCorner.Application.Exceptions;

public class ValidationException : Exception
{
    public ValidationException(string message, ValidationResult validationResult) : base(message)
    {
        ValidationErrors = validationResult.ToDictionary();
    }

    public IDictionary<string, string[]> ValidationErrors { get; }
}