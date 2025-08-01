using TalkCorner.Domain.Common;

namespace TalkCorner.Domain.ValueObjects;

/// <summary>
///     Encapsulates the content of a post, enforcing validation rules.
/// </summary>
public sealed class PostContent : ValueObject
{
    private PostContent()
    {
    }

    private PostContent(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static PostContent Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Post content cannot be empty.", nameof(value));
        }

        if (value.Length > 5000)
        {
            throw new ArgumentException("Post content exceeds maximum length of 5000.", nameof(value));
        }

        return new PostContent(value.Trim());
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