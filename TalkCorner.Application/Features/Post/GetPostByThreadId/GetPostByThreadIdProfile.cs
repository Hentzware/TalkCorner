using AutoMapper;

namespace TalkCorner.Application.Features.Post.GetPostByThreadId;

public class GetPostByThreadIdProfile : Profile
{
    public GetPostByThreadIdProfile()
    {
        CreateMap<Domain.Entities.Post, GetPostByThreadIdDto>()
            .ForMember(dest => dest.CreatedByUsername, opt => opt.MapFrom(src => src.CreatedByUser.DisplayName));
    }
}