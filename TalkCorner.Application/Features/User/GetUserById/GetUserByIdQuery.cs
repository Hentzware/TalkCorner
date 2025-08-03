using MediatR;

namespace TalkCorner.Application.Features.User.GetUserById;

public record GetUserByIdQuery(Guid Id) : IRequest<GetUserByIdDto>;