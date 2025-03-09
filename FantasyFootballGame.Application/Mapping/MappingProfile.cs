using AutoMapper;
using FantasyFootballGame.Application.DTOs.Players;
using FantasyFootballGame.Application.DTOs.Teams;
using FantasyFootballGame.Domain.Models;

namespace FantasyFootballGame.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        { 
            CreateMap<Team,TeamResponseDto>();
            CreateMap<CreateTeamDto, Team>();
            CreateMap<UpdateTeamDto, Team>();

            CreateMap<Player, PlayerResponseDto>();
            CreateMap<CreatePlayerDto, Player>();
            CreateMap<UpdatePlayerDto, Player>();
        }
    }
}
