using AutoMapper;
using MediatR;
using TalkCorner.Application.Contracts.Persistence;

namespace TalkCorner.Application.Features.User.GetUserById;

public class GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper) : IRequestHandler<GetUserByIdQuery, GetUserByIdDto>
{
    public async Task<GetUserByIdDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdWithTrackingAsync(request.Id, cancellationToken);

        if (user == null)
        {
            throw new InvalidOperationException("User does not exist.");
        }

        var dto = mapper.Map<GetUserByIdDto>(user);
        return dto;
    }
}