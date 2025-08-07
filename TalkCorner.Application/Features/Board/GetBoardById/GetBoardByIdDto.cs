namespace TalkCorner.Application.Features.Board.GetBoardById;

public class GetBoardByIdDto
{
    public Guid CreatedByUserId { get; set; }

    public Guid Id { get; set; }

    public Guid? ParentBoardId { get; set; }

    public int PostCount { get; set; }

    public int ThreadCount { get; set; }

    public int SortOrder { get; set; }

    public List<GetBoardByIdBoardListItemDto> SubBoards { get; set; } = [];

    public List<GetBoardByIdThreadListItemDto> Threads { get; set; } = [];

    public List<GetBoardByIdUserDto> Moderators { get; set; } = [];

    public string CreatedByUsername { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? ParentBoardTitle { get; set; }
}