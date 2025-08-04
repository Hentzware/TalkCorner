namespace TalkCorner.Application.Features.Authentication.Common;

public class ApplicationUserDto
{
    public Guid Id { get; set; }

    public string Email { get; set; } = string.Empty;
}