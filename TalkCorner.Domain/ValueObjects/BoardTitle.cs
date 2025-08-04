using TalkCorner.Domain.Common;

namespace TalkCorner.Domain.ValueObjects;

public sealed class BoardTitle : ValueObject
{
    private BoardTitle()
    {
    }

    private BoardTitle(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static BoardTitle Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Der Board-Titel darf nicht leer sein.", nameof(value));
        }

        if (value.Length > 100)
        {
            throw new ArgumentException("Der Board-Titel darf höchstens 100 Zeichen lang sein.", nameof(value));
        }

        // Weitere fachliche Regeln (verbotene Wörter etc.) können hier geprüft werden.

        return new BoardTitle(value);
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