using AutoMapper;

namespace TalkCorner.Application.Features.Board.GetAllBoards;

public class GetAllBoardsProfile : Profile
{
    public GetAllBoardsProfile()
    {
        CreateMap<Domain.Entities.Board, GetAllBoardsDto>()
            .ForMember(dest => dest.ThreadCount, opt => opt.MapFrom(src => src.Threads.Count))
            .ForMember(dest => dest.SubBoardCount, opt => opt.MapFrom(src => src.SubBoards.Count));
    }
}