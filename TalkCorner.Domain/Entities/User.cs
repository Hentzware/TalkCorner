using System.ComponentModel.DataAnnotations.Schema;
using TalkCorner.Domain.Common;
using TalkCorner.Domain.ValueObjects;

namespace TalkCorner.Domain.Entities;

public class User : BaseEntity
{
    private readonly List<Board> _createdBoards = new();
    private readonly List<Board> _moderatedBoards = new();
    private readonly List<Post> _posts = new();
    private readonly List<Thread> _threads = new();

    private User()
    {
    }

    private User(Guid applicationUserId, DisplayName displayName)
    {
        ApplicationUserId = applicationUserId;
        DisplayName = displayName;
    }

    private User(Guid id, Guid applicationUserId, DisplayName displayName)
    {
        Id = id;
        ApplicationUserId = applicationUserId;
        DisplayName = displayName;
    }

    public DisplayName DisplayName { get; private set; } = null!;

    public Guid ApplicationUserId { get; private set; }

    [InverseProperty(nameof(Board.CreatedByUser))]
    public IReadOnlyCollection<Board> CreatedBoards => _createdBoards.AsReadOnly();

    [InverseProperty(nameof(Board.Moderators))]
    public IReadOnlyCollection<Board> ModeratedBoards => _moderatedBoards.AsReadOnly();

    [InverseProperty(nameof(Post.CreatedByUser))]
    public IReadOnlyCollection<Post> Posts => _posts.AsReadOnly();

    [InverseProperty(nameof(Thread.CreatedByUser))]
    public IReadOnlyCollection<Thread> Threads => _threads.AsReadOnly();

    public static User Create(Guid applicationUserId, string displayName)
    {
        var dn = DisplayName.Create(displayName);
        return new User(applicationUserId, dn);
    }

    public static User Create(Guid id, Guid applicationUserId, string displayName)
    {
        var dn = DisplayName.Create(displayName);
        return new User(id, applicationUserId, dn);
    }

    public void UpdateDisplayName(string newDisplayName)
    {
        DisplayName = DisplayName.Create(newDisplayName);
    }

    public void AddCreatedBoard(Board board)
    {
        if (board == null)
        {
            throw new ArgumentNullException(nameof(board));
        }

        if (_createdBoards.Contains(board))
        {
            throw new InvalidOperationException("Board already associated.");
        }

        _createdBoards.Add(board);
    }

    public void AddThread(Thread thread)
    {
        if (thread == null)
        {
            throw new ArgumentNullException(nameof(thread));
        }

        if (_threads.Contains(thread))
        {
            throw new InvalidOperationException("Thread already associated.");
        }

        _threads.Add(thread);
    }

    public void AddPost(Post post)
    {
        if (post == null)
        {
            throw new ArgumentNullException(nameof(post));
        }

        if (_posts.Contains(post))
        {
            throw new InvalidOperationException("Post already associated.");
        }

        _posts.Add(post);
    }

    public bool IsModeratorOf(Board board)
    {
        return board.Moderators.Contains(this);
    }
}