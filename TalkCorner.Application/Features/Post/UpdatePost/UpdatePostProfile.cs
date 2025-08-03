using AutoMapper;

namespace TalkCorner.Application.Features.Post.UpdatePost;

public class UpdatePostProfile : Profile
{
    public UpdatePostProfile()
    {
        CreateMap<UpdatePostCommand, Domain.Entities.Post>();
    }
}