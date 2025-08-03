using AutoMapper;

namespace TalkCorner.Application.Features.Post.CreatePost;

public class CreatePostProfile : Profile
{
    public CreatePostProfile()
    {
        CreateMap<CreatePostCommand, Domain.Entities.Post>();
    }
}