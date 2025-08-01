using TalkCorner.Domain.Common;

namespace TalkCorner.Domain.ValueObjects;

/// <summary>
///     Represents a user's display name. Immutable value object,
///     enforces domain-specific rules and compares by value.
/// </summary>
public sealed class DisplayName : ValueObject
{
    private DisplayName(){}

    private DisplayName(string value)
    {
        Value = value;
    }

    public string Value { get; }

    /// <summary>
    ///     Factory method to create a new DisplayName after validating input.
    /// </summary>
    /// <param name="value">The display name string (non-null, length constraints).</param>
    /// <returns>A valid DisplayName instance.</returns>
    /// <exception cref="ArgumentException">If display name is invalid.</exception>
    public static DisplayName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("DisplayName cannot be empty.", nameof(value));
        }

        if (value.Trim().Length < 3 || value.Trim().Length > 100)
        {
            throw new ArgumentException("DisplayName length must be between 3 and 100 characters.", nameof(value));
        }

        return new DisplayName(value.Trim());
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