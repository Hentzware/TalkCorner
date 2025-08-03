using AutoMapper;

namespace TalkCorner.Application.Features.Thread.GetThreadsByBoardId;

public class GetThreadsByBoardIdProfile : Profile
{
    public GetThreadsByBoardIdProfile()
    {
        CreateMap<Domain.Entities.Thread, GetThreadsByBoardIdDto>()
            .ForMember(dest => dest.CreatedByUsername, opt => opt.MapFrom(src => src.CreatedByUser.DisplayName))
            .ForMember(dest => dest.PostCount, opt => opt.MapFrom(src => src.Posts.Count));
    }
}