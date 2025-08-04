namespace TalkCorner.Application.Contracts.Common;

public interface IUserContextAware
{
    Guid CurrentUserId { get; set; }
}