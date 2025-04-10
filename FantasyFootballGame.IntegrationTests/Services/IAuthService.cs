using FantasyFootballGame.Domain.Enums.User;

namespace FantasyFootballGame.IntegrationTests.Services
{
    public interface IAuthService
    {
        Task<string> GetTokenAsync(UserRole role);
    }
}
