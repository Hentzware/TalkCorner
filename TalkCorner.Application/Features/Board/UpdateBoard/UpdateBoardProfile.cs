using AutoMapper;

namespace TalkCorner.Application.Features.Board.UpdateBoard;

public class UpdateBoardProfile : Profile
{
    public UpdateBoardProfile()
    {
        CreateMap<UpdateBoardCommand, Domain.Entities.Board>();
    }
}