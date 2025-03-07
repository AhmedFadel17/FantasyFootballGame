using System.Text.Json.Serialization;

namespace FantasyFootballGame.Domain.Models
{
    public record Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public string MainColor { get; set; }
        public string SecondaryColor { get; set; }
        public string ShirtImgSrc { get; set; }

        [JsonIgnore]
        public IEnumerable<Player>? Players { get; set; }


    }
}
