using TalkCorner.Domain.Common;

namespace TalkCorner.Domain.ValueObjects;

public sealed class DisplayName : ValueObject
{
    public const int MaxLength = 32;

    public const int MinLength = 2;

    private DisplayName()
    {
        Value = string.Empty;
    }

    private DisplayName(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static DisplayName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("DisplayName must not be empty.", nameof(value));
        }

        if (value.Length < MinLength)
        {
            throw new ArgumentException($"DisplayName must be at least {MinLength} characters long.", nameof(value));
        }

        if (value.Length > MaxLength)
        {
            throw new ArgumentException($"DisplayName must be at most {MaxLength} characters long.", nameof(value));
        }

        if (!value.All(c => char.IsLetterOrDigit(c) || c == '_' || c == '-'))
        {
            throw new ArgumentException("DisplayName may only contain letters, digits, underscores and dashes.", nameof(value));
        }

        return new DisplayName(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value;
    }
}