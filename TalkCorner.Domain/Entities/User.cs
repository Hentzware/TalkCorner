using System.ComponentModel.DataAnnotations.Schema;
using TalkCorner.Domain.Common;
using TalkCorner.Domain.ValueObjects;

namespace TalkCorner.Domain.Entities;

public class User : BaseEntity
{
    public User()
    {
    }

    private User(Guid applicationUserId, DisplayName displayName)
    {
        ApplicationUserId = applicationUserId;
        DisplayName = displayName;
    }

    public DisplayName DisplayName { get; private set; } = null!;

    public Guid ApplicationUserId { get; private set; }

    [InverseProperty(nameof(Board.CreatedByUser))]
    public IReadOnlyCollection<Board> CreatedBoards { get; private set; } = new List<Board>();

    [InverseProperty(nameof(Board.Moderators))]
    public IReadOnlyCollection<Board> ModeratedBoards { get; private set; } = new List<Board>();

    [InverseProperty(nameof(Post.CreatedByUser))]
    public IReadOnlyCollection<Post> Posts { get; private set; } = new List<Post>();

    [InverseProperty(nameof(Thread.CreatedByUser))]
    public IReadOnlyCollection<Thread> Threads { get; private set; } = new List<Thread>();

    public static User Create(Guid applicationUserId, string displayName)
    {
        if (applicationUserId == Guid.Empty)
        {
            throw new ArgumentException("ApplicationUserId must not be empty.", nameof(applicationUserId));
        }

        var dn = DisplayName.Create(displayName);

        return new User(applicationUserId, dn);
    }

    public void UpdateDisplayName(string newDisplayName)
    {
        DisplayName = DisplayName.Create(newDisplayName);
    }
}