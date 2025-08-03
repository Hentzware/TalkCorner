namespace TalkCorner.Application.Features.User.GetUserById;

public class GetUserByIdDto
{
    public bool IsActive { get; set; }

    public DateTime Created { get; set; }

    public DateTime? Updated { get; set; }

    public Guid Id { get; set; }

    public string DisplayName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string UserName { get; set; } = string.Empty;
}