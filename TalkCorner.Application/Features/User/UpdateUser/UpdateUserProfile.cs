using AutoMapper;

namespace TalkCorner.Application.Features.User.UpdateUser;

public class UpdateUserProfile : Profile
{
    public UpdateUserProfile()
    {
        CreateMap<UpdateUserCommand, Domain.Entities.User>();
    }
}