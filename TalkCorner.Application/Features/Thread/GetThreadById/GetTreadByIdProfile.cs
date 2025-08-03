using AutoMapper;

namespace TalkCorner.Application.Features.Thread.GetThreadById;

public class GetThreadByIdProfile : Profile
{
    public GetThreadByIdProfile()
    {
        CreateMap<Domain.Entities.Thread, GetThreadByIdDto>()
            .ForMember(dest => dest.BoardTitle, opt => opt.MapFrom(src => src.Board.Title.Value))
            .ForMember(dest => dest.CreatedByUsername, opt => opt.MapFrom(src => src.CreatedByUser.DisplayName))
            .ForMember(dest => dest.Posts, opt => opt.MapFrom(src => src.Posts));

        CreateMap<Domain.Entities.Post, GetThreadByIdPostListItemDto>()
            .ForMember(dest => dest.CreatedByUsername, opt => opt.MapFrom(src => src.CreatedByUser.DisplayName));
    }
}