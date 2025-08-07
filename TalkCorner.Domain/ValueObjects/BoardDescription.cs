using TalkCorner.Domain.Common;

namespace TalkCorner.Domain.ValueObjects;

public sealed class BoardDescription : ValueObject
{
    private BoardDescription()
    {
        Value = string.Empty;
    }

    private BoardDescription(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static BoardDescription Create(string value)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        if (value.Length > 1000)
        {
            throw new ArgumentException(nameof(value));
        }

        return new BoardDescription(value);
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