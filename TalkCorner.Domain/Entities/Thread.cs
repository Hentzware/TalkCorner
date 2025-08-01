using System.ComponentModel.DataAnnotations.Schema;
using TalkCorner.Domain.Common;
using TalkCorner.Domain.ValueObjects;

namespace TalkCorner.Domain.Entities;

public class Thread : BaseEntity
{
    private readonly List<Post> _posts = new();

    private Thread()
    {
    }

    private Thread(ThreadTitle title, Guid createdByUserId, Guid boardId)
    {
        Title = title;
        CreatedByUserId = createdByUserId;
        BoardId = boardId;
    }

    [ForeignKey(nameof(BoardId))]
    [InverseProperty(nameof(Board.Threads))]
    public Board Board { get; private set; }

    public Guid BoardId { get; private set; }

    public Guid CreatedByUserId { get; private set; }

    [InverseProperty(nameof(Post.Thread))]
    public IReadOnlyCollection<Post> Posts => _posts.AsReadOnly();

    public ThreadTitle Title { get; private set; } = null!;

    [ForeignKey(nameof(CreatedByUserId))]
    [InverseProperty(nameof(User.Threads))]
    public User CreatedByUser { get; private set; }

    public static Thread Create(string title, User createdByUser, Board board)
    {
        if (createdByUser == null)
        {
            throw new ArgumentNullException(nameof(createdByUser));
        }

        if (board == null)
        {
            throw new ArgumentNullException(nameof(board));
        }

        var threadTitle = ThreadTitle.Create(title);

        var thread = new Thread(threadTitle, createdByUser.Id, board.Id);

        // Navigationen setzen
        thread.SetCreatedByUser(createdByUser);
        thread.SetBoard(board);

        // Listen auffüllen
        createdByUser.AddThread(thread);
        board.AddThread(thread);

        return thread;
    }

    private void SetCreatedByUser(User user)
    {
        CreatedByUser = user;
        CreatedByUserId = user.Id;
    }

    private void SetBoard(Board board)
    {
        Board = board;
        BoardId = board.Id;
    }

    public void AddPost(Post post)
    {
        if (post == null)
        {
            throw new ArgumentNullException(nameof(post));
        }

        if (_posts.Contains(post))
        {
            throw new InvalidOperationException("This post already exists in the thread.");
        }

        _posts.Add(post);
    }

    public void UpdateTitle(string newTitle)
    {
        Title = ThreadTitle.Create(newTitle);
    }
}