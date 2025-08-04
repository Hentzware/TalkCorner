using MediatR;
using TalkCorner.Application.Contracts.Persistence;
using TalkCorner.Application.Exceptions;

namespace TalkCorner.Application.Features.User.UpdateUser;

public class UpdateUserCommandHandler(IUserRepository userRepository) : IRequestHandler<UpdateUserCommand, Unit>
{
    public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdWithTrackingAsync(request.Id, cancellationToken);

        if (user == null)
        {
            throw new NotFoundException(nameof(Domain.Entities.User), request.Id);
        }

        user.UpdateDisplayName(request.DisplayName);

        await userRepository.UpdateAsync(user, cancellationToken);
        await userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}