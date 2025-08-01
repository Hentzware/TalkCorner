using System.ComponentModel.DataAnnotations.Schema;
using TalkCorner.Domain.Common;
using TalkCorner.Domain.ValueObjects;

namespace TalkCorner.Domain.Entities;

public class Board : BaseEntity
{
    private readonly List<Board> _subBoards = new();
    private readonly List<Thread> _threads = new();
    private readonly List<User> _moderators = new();

    private Board()
    {
    }

    private Board(BoardTitle title, BoardDescription description, Guid createdByUserId)
    {
        Title = title;
        Description = description;
        CreatedByUserId = createdByUserId;
    }

    [ForeignKey(nameof(ParentBoardId))]
    [InverseProperty(nameof(SubBoards))]
    public Board? ParentBoard { get; private set; }

    public BoardDescription Description { get; private set; } = null!;

    public BoardTitle Title { get; private set; } = null!;

    public Guid CreatedByUserId { get; private set; }

    public Guid? ParentBoardId { get; private set; }

    [InverseProperty(nameof(ParentBoard))]
    public IReadOnlyCollection<Board> SubBoards => _subBoards.AsReadOnly();

    [InverseProperty(nameof(Thread.Board))]
    public IReadOnlyCollection<Thread> Threads => _threads.AsReadOnly();

    [InverseProperty(nameof(User.ModeratedBoards))]
    public IReadOnlyCollection<User> Moderators => _moderators.AsReadOnly();

    [ForeignKey(nameof(CreatedByUserId))]
    [InverseProperty(nameof(User.CreatedBoards))]
    public User CreatedByUser { get; private set; } = null!;

    public void AddThread(Thread thread)
    {
        if (thread == null)
        {
            throw new ArgumentNullException(nameof(thread), "Thread must not be null.");
        }

        if (_threads.Contains(thread))
        {
            throw new InvalidOperationException("Thread is already added to the board.");
        }

        _threads.Add(thread);
    }

    public void RemoveThread(Thread thread)
    {
        if (!_threads.Contains(thread))
        {
            throw new InvalidOperationException("Thread not found in the board.");
        }

        _threads.Remove(thread);
    }

    public void AddModerator(User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user), "User must not be null.");
        }

        if (_moderators.Contains(user))
        {
            throw new InvalidOperationException("User is already a moderator.");
        }

        _moderators.Add(user);
    }

    public void RemoveModerator(User user)
    {
        if (!_moderators.Contains(user))
        {
            throw new InvalidOperationException("User is not a moderator.");
        }

        _moderators.Remove(user);
    }

    public void AddSubBoard(Board subBoard)
    {
        if (subBoard == null)
        {
            throw new ArgumentNullException(nameof(subBoard), "Sub-board must not be null.");
        }

        if (_subBoards.Contains(subBoard))
        {
            throw new InvalidOperationException("The board is already added as a sub-board.");
        }

        subBoard.SetParentBoard(this);
        _subBoards.Add(subBoard);
    }

    public void RemoveSubBoard(Board subBoard)
    {
        if (subBoard == null)
        {
            throw new ArgumentNullException(nameof(subBoard), "Sub-board must not be null.");
        }

        if (!_subBoards.Contains(subBoard))
        {
            throw new InvalidOperationException("The board does not contain the sub-board.");
        }

        _subBoards.Remove(subBoard);
        subBoard.ClearParentBoard();
    }

    private void ClearParentBoard()
    {
        ParentBoard = null;
        ParentBoardId = null;
    }

    private void SetParentBoard(Board parent)
    {
        ParentBoard = parent;
        ParentBoardId = parent.Id;
    }

    public static Board Create(string title, string description, User createdByUser)
    {
        if (createdByUser == null)
            throw new ArgumentNullException(nameof(createdByUser));

        var boardTitle = BoardTitle.Create(title);
        var boardDescription = BoardDescription.Create(description);

        var board = new Board(boardTitle, boardDescription, createdByUser.Id);

        // Setze Navigation explizit
        board.SetCreatedByUser(createdByUser);

        return board;
    }

    public void UpdateTitle(string newTitle)
    {
        Title = BoardTitle.Create(newTitle);
    }

    public void UpdateDescription(string newDescription)
    {
        Description = BoardDescription.Create(newDescription);
    }

    // z.B. als interne Methode oder Property-Setter:
    private void SetCreatedByUser(User user)
    {
        CreatedByUser = user;
        CreatedByUserId = user.Id;
    }
}