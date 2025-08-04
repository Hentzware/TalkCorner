using AutoMapper;
using MediatR;
using TalkCorner.Application.Contracts.Identity;
using TalkCorner.Application.Contracts.Persistence;
using TalkCorner.Application.Exceptions;
using TalkCorner.Application.Features.Authentication.Common;

namespace TalkCorner.Application.Features.User.GetUserById;

public class GetUserByIdQueryHandler(IUserRepository userRepository, IApplicationUserRepository applicationUserRepository, IMapper mapper) : IRequestHandler<GetUserByIdQuery, GetUserByIdDto>
{
    public async Task<GetUserByIdDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdWithTrackingAsync(request.Id, cancellationToken);
        
        if (user == null)
        {
            throw new NotFoundException(nameof(User), request.Id);
        }

        var applicationUser = await applicationUserRepository.GetApplicationUserByIdAsync(user.ApplicationUserId);
        var response = mapper.Map<GetUserByIdDto>(user);

        if (applicationUser == null)
        {
            throw new NotFoundException(nameof(ApplicationUserDto), user.ApplicationUserId);
        }

        response.Email = applicationUser.Email;

        return response;
    }
}