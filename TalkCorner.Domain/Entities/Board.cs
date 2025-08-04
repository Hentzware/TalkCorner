using TalkCorner.Domain.Common;
using TalkCorner.Domain.ValueObjects;

namespace TalkCorner.Domain.Entities;

public class Board : BaseEntity
{
    private Board()
    {
    }

    private Board(BoardTitle title, BoardDescription description, Guid createdByUserId, Guid? parentBoardId = null)
    {
        Title = title;
        Description = description;
        CreatedByUserId = createdByUserId;
        ParentBoardId = parentBoardId;
    }

    public Board? ParentBoard { get; private set; }

    public BoardDescription Description { get; private set; } = null!;

    public BoardTitle Title { get; private set; } = null!;

    public Guid CreatedByUserId { get; private set; }

    public Guid? ParentBoardId { get; private set; }

    public IReadOnlyCollection<Board> SubBoards { get; private set; } = new List<Board>();

    public IReadOnlyCollection<Thread> Threads { get; private set; } = new List<Thread>();

    public IReadOnlyCollection<User> Moderators { get; private set; } = new List<User>();

    public User CreatedByUser { get; private set; } = null!;

    public static Board Create(string title, string description, Guid createdByUserId, Guid? parentBoardId = null)
    {
        var boardTitle = BoardTitle.Create(title);
        var boardDescription = BoardDescription.Create(description);
        return new Board(boardTitle, boardDescription, createdByUserId, parentBoardId);
    }

    public void UpdateTitle(string newTitle)
    {
        Title = BoardTitle.Create(newTitle);
    }

    public void UpdateDescription(string newDescription)
    {
        Description = BoardDescription.Create(newDescription);
    }
}