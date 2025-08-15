namespace TalkCorner.Domain.Common;

public abstract class BaseEntity
{
    public DateTime Created { get; set; }

    public DateTime Updated { get; set; }

    public Guid Id { get; set; }
}