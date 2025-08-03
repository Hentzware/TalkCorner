using AutoMapper;

namespace TalkCorner.Application.Features.User.GetAllUsers;

public class GetAllUsersProfile : Profile
{
    public GetAllUsersProfile()
    {
        CreateMap<Domain.Entities.User, GetAllUsersDto>();
    }
}