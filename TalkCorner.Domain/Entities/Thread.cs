using TalkCorner.Domain.Common;
using TalkCorner.Domain.ValueObjects;

namespace TalkCorner.Domain.Entities;

public class Thread : BaseEntity
{
    private Thread()
    {
    }

    private Thread(ThreadTitle title, Guid createdByUserId, Guid boardId)
    {
        Title = title;
        CreatedByUserId = createdByUserId;
        BoardId = boardId;
    }

    public Board Board { get; private set; } = null!;

    public bool IsClosed { get; private set; }

    public bool IsSticky { get; private set; }

    public Guid BoardId { get; private set; }

    public Guid CreatedByUserId { get; private set; }

    public IReadOnlyCollection<Post> Posts { get; private set; } = new List<Post>();

    public ThreadTitle Title { get; private set; } = null!;

    public User CreatedByUser { get; private set; } = null!;

    public static Thread Create(string title, Guid createdByUserId, Guid boardId)
    {
        var threadTitle = ThreadTitle.Create(title);
        return new Thread(threadTitle, createdByUserId, boardId);
    }

    public void Close()
    {
        IsClosed = true;
    }

    public void MoveToBoard(Guid newBoardId)
    {
        BoardId = newBoardId;
    }

    public void Open()
    {
        IsClosed = false;
    }

    public void Stick()
    {
        IsSticky = true;
    }

    public void Unstick()
    {
        IsSticky = false;
    }

    public void UpdateTitle(string newTitle)
    {
        Title = ThreadTitle.Create(newTitle);
    }
}