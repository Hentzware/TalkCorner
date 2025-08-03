using AutoMapper;
using MediatR;
using TalkCorner.Application.Contracts.Persistence;

namespace TalkCorner.Application.Features.User.GetAllUsers;

public class GetAllUsersQueryHandler(IUserRepository userRepository, IMapper mapper) : IRequestHandler<GetAllUsersQuery, IEnumerable<GetAllUsersDto>>
{
    public async Task<IEnumerable<GetAllUsersDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await userRepository.GetAsync(cancellationToken);
        var response = mapper.Map<IEnumerable<GetAllUsersDto>>(users);
        return response;
    }
}