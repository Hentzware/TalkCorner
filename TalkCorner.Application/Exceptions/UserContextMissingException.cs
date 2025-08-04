namespace TalkCorner.Application.Exceptions;

public class UserContextMissingException : Exception
{
    public UserContextMissingException(string message) : base(message)
    {
    }
}