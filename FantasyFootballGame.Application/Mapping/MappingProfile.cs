using AutoMapper;
using FantasyFootballGame.Application.DTOs.Fixtures;
using FantasyFootballGame.Application.DTOs.Gameweeks;
using FantasyFootballGame.Application.DTOs.Players;
using FantasyFootballGame.Application.DTOs.Teams;
using FantasyFootballGame.Domain.Models;

namespace FantasyFootballGame.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Gameweek, GameweekResponseDto>();
            CreateMap<CreateGameweekDto, Gameweek>();
            CreateMap<UpdateGameweekDto, Gameweek>();

            CreateMap<Fixture, FixtureResponseDto>();
            CreateMap<CreateFixtureDto, Fixture>();
            CreateMap<UpdateFixtureDto, Fixture>();

            CreateMap<Team,TeamResponseDto>();
            CreateMap<CreateTeamDto, Team>();
            CreateMap<UpdateTeamDto, Team>();

            CreateMap<Player, PlayerResponseDto>();
            CreateMap<CreatePlayerDto, Player>();
            CreateMap<UpdatePlayerDto, Player>();
        }
    }
}
