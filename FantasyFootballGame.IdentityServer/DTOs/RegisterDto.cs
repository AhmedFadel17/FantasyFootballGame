using FantasyFootballGame.IdentityServer.Enums;

namespace FantasyFootballGame.IdentityServer.DTOs
{
    public record RegisterDto
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required UserRole Role { get; set; }
    }
}
