
namespace FantasyFootballGame.Domain.Settings
{
    public class IdentityServerSettings
    {
        public required string Authority { get; set; }
        public required string ClientId { get; set; }
        public required string ClientSecret { get; set; }
        public required string Scope { get; set; }
    }
}
