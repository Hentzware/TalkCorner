using MediatR;
using TalkCorner.Application.Contracts.Persistence;
using TalkCorner.Application.Exceptions;

namespace TalkCorner.Application.Features.User.DeleteUser;

public class DeleteUserCommandHandler(IUserRepository userRepository) : IRequestHandler<DeleteUserCommand, Unit>
{
    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdWithTrackingAsync(request.Id);

        if (user == null)
        {
            throw new NotFoundException(nameof(Domain.Entities.User), request.Id);
        }

        await userRepository.DeleteAsync(user, cancellationToken);
        await userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}