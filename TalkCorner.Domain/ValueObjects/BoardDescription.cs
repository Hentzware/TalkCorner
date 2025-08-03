using TalkCorner.Domain.Common;

namespace TalkCorner.Domain.ValueObjects;

public sealed class BoardDescription : ValueObject
{
    private BoardDescription()
    {
    }

    private BoardDescription(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static BoardDescription Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Die Board-Beschreibung darf nicht leer sein.", nameof(value));
        }

        if (value.Length > 1000)
        {
            throw new ArgumentException("Die Board-Beschreibung darf höchstens 1 000 Zeichen lang sein.", nameof(value));
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