using FluentValidation.Results;

namespace TalkCorner.Application.Exceptions;

public class ValidationException(string message, ValidationResult validationResult) : Exception(message)
{
    public IDictionary<string, string[]> ValidationErrors { get; } = validationResult.ToDictionary();
}