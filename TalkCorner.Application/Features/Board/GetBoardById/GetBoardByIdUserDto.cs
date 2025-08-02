namespace TalkCorner.Application.Features.Board.GetBoardById;

public class GetBoardByIdUserDto
{
    public Guid Id { get; set; }

    public string DisplayName { get; set; } = null!;
}