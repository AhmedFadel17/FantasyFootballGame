using CsvHelper;
using CsvHelper.Configuration;
using FantasyFootballGame.DataAccess.Repositories.Players;
using FantasyFootballGame.DataAccess.Repositories.Teams;
using FantasyFootballGame.Domain.Models;
using FantasyFootballGame.Domain.Enums;
using System.Globalization;

namespace FantasyFootballGame.DataAccess.Seeds
{
    public class LaLigaPlayerSeeder
    {
        private readonly ITeamsRepository _teamsRepository;
        private readonly IPlayersRepository _playersRepository;
        private readonly string _csvPath;

        public LaLigaPlayerSeeder(
            ITeamsRepository teamsRepository,
            IPlayersRepository playersRepository,
            string csvPath = "./../FantasyFootballGame.DataAccess/Seeds/male_players.csv")
        {
            _teamsRepository = teamsRepository;
            _playersRepository = playersRepository;
            _csvPath = csvPath;
        }

        public class PlayerCsvRecord
        {
            public string Name { get; set; } = string.Empty;
            public string Team { get; set; } = string.Empty;
            public string League { get; set; } = string.Empty;
            public string Position { get; set; } = string.Empty;
            public int OVR { get; set; }
        }
        public async Task SeedAsync()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null,
                MissingFieldFound = null
            };

            using var reader = new StreamReader(_csvPath);
            using var csv = new CsvReader(reader, config);
            var records = csv.GetRecords<PlayerCsvRecord>().ToList();

            foreach (var record in records)
            {
                if (record.League != "LALIGA EA SPORTS")
                    continue;

                // Check if team exists, if not create it
                var team = (await _teamsRepository.GetAll()).FirstOrDefault(t => t.Name == record.Team);
                if (team == null)
                {
                    team = new Team
                    {
                        Name = record.Team,
                        Abbreviation = "000",
                        MainColor = "#000000",
                        SecondaryColor = "#000000",
                        ShirtImgSrc = $"https://www.laliga.com/sites/default/files/styles/2xl/public/2024-25/teams/logos/{record.Team.ToLower()}.png"
                    };
                    await _teamsRepository.Create(team);
                    await _teamsRepository.Save();
                }

                // Check if player already exists
                var existingPlayer = (await _playersRepository.GetAll()).FirstOrDefault(p => p.FullName == record.Name);
                if (existingPlayer != null)
                    continue;

                var playerPosition = GetPlayerPosition(record.Position);
                var playerPrice = GetPlayerPrice(record.OVR, playerPosition);
                // Create new player
                var player = new Player
                {
                    Name = record.Name,
                    FullName = record.Name,
                    TeamId = team.Id,
                    Position = playerPosition,
                    Price = playerPrice
                };

                await _playersRepository.Create(player);
            }

            await _playersRepository.Save();
        }

        public double GetPlayerPrice(int overall,PlayerPosition position)
        {
            return (overall, position) switch
            {
                (>= 90, PlayerPosition.Goalkeeper) => 6.5,
                (>= 86, PlayerPosition.Goalkeeper) => 6,
                (>= 85, PlayerPosition.Goalkeeper) => 5.5,
                (>= 82, PlayerPosition.Goalkeeper) => 5,
                (>= 80, PlayerPosition.Goalkeeper) => 4.5,
                (>= 0, PlayerPosition.Goalkeeper) => 4,
                (>= 90, PlayerPosition.Defender) => 7,
                (>= 86, PlayerPosition.Defender) => 6.5,
                (>= 85, PlayerPosition.Defender) => 6,
                (>= 82, PlayerPosition.Defender) => 5.5,
                (>= 80, PlayerPosition.Defender) => 5,
                (>= 0, PlayerPosition.Defender) => 4,
                (>= 90, PlayerPosition.Midfielder) => 11,
                (>= 88, PlayerPosition.Midfielder) => 10,
                (>= 87, PlayerPosition.Midfielder) => 9.5,
                (>= 86, PlayerPosition.Midfielder) => 9,
                (>= 85, PlayerPosition.Midfielder) => 8,
                (>= 84, PlayerPosition.Midfielder) => 7,
                (>= 82, PlayerPosition.Midfielder) => 6.5,
                (>= 80, PlayerPosition.Midfielder) => 6,
                (>= 75, PlayerPosition.Midfielder) => 5.5,
                (>= 70, PlayerPosition.Midfielder) => 5,
                (>= 0, PlayerPosition.Midfielder) => 4.5,
                (>= 90, PlayerPosition.Forward) => 15,
                (>= 88, PlayerPosition.Forward) => 12,
                (>= 87, PlayerPosition.Forward) => 11,
                (>= 86, PlayerPosition.Forward) => 10,
                (>= 85, PlayerPosition.Forward) => 9,
                (>= 84, PlayerPosition.Forward) => 8.5,
                (>= 82, PlayerPosition.Forward) => 8,
                (>= 80, PlayerPosition.Forward) => 7,
                (>= 75, PlayerPosition.Forward) => 6,
                (>= 70, PlayerPosition.Forward) => 5,
                (>= 0, PlayerPosition.Forward) => 4.5,
                _ => 4.5
            };
        }

        public PlayerPosition GetPlayerPosition(string position)
        {
            return position switch
            {
                "GK" => PlayerPosition.Goalkeeper,
                "CB" => PlayerPosition.Defender,
                "RB" => PlayerPosition.Defender,
                "LB" => PlayerPosition.Defender,
                "CM" => PlayerPosition.Midfielder,
                "CDM" => PlayerPosition.Midfielder,
                "CAM" => PlayerPosition.Midfielder,
                "RM" => PlayerPosition.Midfielder,
                "LM" => PlayerPosition.Midfielder,
                "RW" => PlayerPosition.Forward,
                "LW" => PlayerPosition.Forward,
                "ST" => PlayerPosition.Forward,
                _ => throw new ArgumentException($"Invalid player position: {position}")
            };
        }
    }
} 