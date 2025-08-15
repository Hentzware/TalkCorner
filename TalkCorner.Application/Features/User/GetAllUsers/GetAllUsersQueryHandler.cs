using AutoMapper;
using MediatR;
using TalkCorner.Application.Contracts.Identity;
using TalkCorner.Application.Contracts.Persistence;

namespace TalkCorner.Application.Features.User.GetAllUsers;

public class GetAllUsersQueryHandler(IUserRepository userRepository, IApplicationUserRepository applicationUserRepository, IMapper mapper) : IRequestHandler<GetAllUsersQuery, IEnumerable<GetAllUsersDto>>
{
    public async Task<IEnumerable<GetAllUsersDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await userRepository.GetAsync(cancellationToken);
        var applicationUsers = await applicationUserRepository.GetAllApplicationUsersAsync();
        
        var response = users.Join(
            applicationUsers,
            user => user.ApplicationUserId,
            appUser => appUser.Id,
            (user, appUser) => new GetAllUsersDto
            {
                Id = user.Id,
                DisplayName = user.DisplayName.Value,
                Created = user.Created,
                Updated = user.Updated,
                Email = appUser.Email
            }
        );

        return response;
    }
}