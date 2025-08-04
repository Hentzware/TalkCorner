namespace TalkCorner.Application.Exceptions;

public class ServiceUnavailableException : Exception
{
    public ServiceUnavailableException(string message = "Service is currently unavailable.") : base(message)
    {
    }
}