using AutoMapper;

namespace TalkCorner.Application.Features.Post.GetPostById;

public class GetPostByIdProfile : Profile
{
    public GetPostByIdProfile()
    {
        CreateMap<Domain.Entities.Post, GetPostByIdDto>()
            .ForMember(dest => dest.ThreadTitle, opt => opt.MapFrom(src => src.Thread.Title.Value))
            .ForMember(dest => dest.CreatedByUsername, opt => opt.MapFrom(src => src.CreatedByUser.DisplayName))
            .ForMember(dest => dest.ParentPostPreview, opt => opt.MapFrom(src => src.ParentPost != null ? src.ParentPost.Content.Value[..Math.Min(src.ParentPost.Content.Value.Length, 50)] : null));
    }
}