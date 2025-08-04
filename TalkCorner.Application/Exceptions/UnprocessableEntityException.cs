namespace TalkCorner.Application.Exceptions;

public class UnprocessableEntityException : Exception
{
    public UnprocessableEntityException(string message) : base(message)
    {
        Errors = new Dictionary<string, string[]>();
    }

    public UnprocessableEntityException(string message, IDictionary<string, string[]> errors) : base(message)
    {
        Errors = errors ?? new Dictionary<string, string[]>();
    }

    public IDictionary<string, string[]>? Errors { get; }
}