using AutoMapper;
using TalkCorner.Domain.Entities;

namespace TalkCorner.Application.Features.Post.GetPostById;

public class GetPostByIdProfile : Profile
{
    public GetPostByIdProfile()
    {
        CreateMap<Domain.Entities.Post, GetPostByIdDto>()
            .ForMember(dest => dest.ThreadTitle, opt => opt.MapFrom(src => src.Thread.Title.Value))
            .ForMember(dest => dest.CreatedByUsername, opt => opt.MapFrom(src => src.CreatedByUser.DisplayName))
            .ForMember(dest => dest.ParentPostPreview, opt => opt.MapFrom(src => GetPreview(src.ParentPost)));
    }

    private static string? GetPreview(Domain.Entities.Post? parentPost)
    {
        if (parentPost == null || parentPost.Content == null || string.IsNullOrEmpty(parentPost.Content.Value))
            return null;
        var content = parentPost.Content.Value;
        return content.Length > 50 ? content.Substring(0, 50) : content;
    }
}