namespace TalkCorner.Application.Contracts.Identity;

public interface IApplicationUserRepository
{
    Task<bool> EmailExistsAsync(string email);
}