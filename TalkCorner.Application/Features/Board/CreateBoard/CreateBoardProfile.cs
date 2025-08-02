using AutoMapper;

namespace TalkCorner.Application.Features.Board.CreateBoard;

public class CreateBoardProfile : Profile
{
    public CreateBoardProfile()
    {
        CreateMap<CreateBoardCommand, Domain.Entities.Board>();
    }
}