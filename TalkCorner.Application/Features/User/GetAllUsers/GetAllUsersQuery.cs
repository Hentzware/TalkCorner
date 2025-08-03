using MediatR;

namespace TalkCorner.Application.Features.User.GetAllUsers;

public record GetAllUsersQuery : IRequest<IEnumerable<GetAllUsersDto>>;