
namespace FantasyFootballGame.Domain.Settings
{
    public class IdentityServerSettings
    {
        public required string Authority { get; set; }
        public required string ClientId { get; set; }
        public required string ClientSecret { get; set; }
        public required string Scope { get; set; }
        public string GrantType { get; set; }
        public List<TestUser> TestUsers { get; set; }
    }

    public class TestUser
    {
        public string Role { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
