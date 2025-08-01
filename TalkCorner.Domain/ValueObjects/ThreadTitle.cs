using TalkCorner.Domain.Common;

namespace TalkCorner.Domain.ValueObjects;

public sealed class ThreadTitle : ValueObject
{
    private ThreadTitle()
    {
    }

    private ThreadTitle(string v)
    {
        Value = v;
    }

    public string Value { get; }

    public static ThreadTitle Create(string v)
    {
        if (string.IsNullOrWhiteSpace(v))
        {
            throw new ArgumentException("Thread title cannot be empty.", nameof(v));
        }

        if (v.Length > 200)
        {
            throw new ArgumentException("Thread title max length is 200.", nameof(v));
        }

        return new ThreadTitle(v.Trim());
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