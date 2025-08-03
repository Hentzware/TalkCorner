using AutoMapper;

namespace TalkCorner.Application.Features.User.GetUserById;

public class GetUserByIdProfile : Profile
{
    public GetUserByIdProfile()
    {
        CreateMap<Domain.Entities.User, GetUserByIdDto>();
    }
}