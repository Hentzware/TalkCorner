using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TalkCorner.Application.Contracts.Common;

namespace TalkCorner.Application.Behaviors;

public class UserContextBehavior<TRequest, TResponse>(IHttpContextAccessor httpContextAccessor) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var user = httpContextAccessor.HttpContext?.User;

        if (request is IUserContextAware userContextAware)
        {
            if (user == null || !user.Identity?.IsAuthenticated == true)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (Guid.TryParse(userIdClaim, out var userId))
            {
                userContextAware.CurrentUserId = userId;
            }
        }

        return await next(cancellationToken);
    }
}