using AutoMapper;
using MediatR;
using TalkCorner.Application.Contracts.Persistence;

namespace TalkCorner.Application.Features.User.UpdateUser;

public class UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper) : IRequestHandler<UpdateUserCommand, Unit>
{
    private readonly IMapper mapper = mapper;

    public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdWithTrackingAsync(request.Id, cancellationToken);

        if (user == null)
        {
            throw new InvalidOperationException("User does not exist.");
        }

        user.UpdateDisplayName(request.DisplayName);

        await userRepository.UpdateAsync(user, cancellationToken);
        await userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}