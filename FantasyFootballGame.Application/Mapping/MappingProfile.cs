using AutoMapper;
using FantasyFootballGame.Application.DTOs.FantasyTeamPlayers;
using FantasyFootballGame.Application.DTOs.FantasyTeams;
using FantasyFootballGame.Application.DTOs.Fixtures;
using FantasyFootballGame.Application.DTOs.Gameweeks;
using FantasyFootballGame.Application.DTOs.Goals;
using FantasyFootballGame.Application.DTOs.Goals.Assists;
using FantasyFootballGame.Application.DTOs.Goals.GoalsScored;
using FantasyFootballGame.Application.DTOs.Goals.OwnGoals;
using FantasyFootballGame.Application.DTOs.Players;
using FantasyFootballGame.Application.DTOs.Teams;
using FantasyFootballGame.Domain.Models;
using FantasyFootballGame.Domain.Models.Actions;

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

            CreateMap<FantasyTeam, FantasyTeamResponseDto>();
            CreateMap<CreateFantasyTeamDto, FantasyTeam>();
            CreateMap<UpdateFantasyTeamDto, FantasyTeam>();

            CreateMap<FantasyTeamPlayer, FantasyTeamPlayerResponseDto>();
            CreateMap<(int teamId,CreateFantasyTeamPlayerDto), FantasyTeamPlayer>()
                .ForMember(dest => dest.GameweekTeamId,opt =>opt.MapFrom(src => src.teamId));
            CreateMap<UpdateFantasyTeamPlayerDto, FantasyTeamPlayer>();

            CreateMap<Goal, GoalResponseDto>();
            CreateMap<CreateGoalDto, Goal>();
            CreateMap<UpdateGoalDto, Goal>();

            CreateMap<GoalScored, GoalScoredResponseDto>();
            CreateMap<CreateGoalScoredDto, GoalScored>();
            CreateMap<(int goalId, CreateGoalScoredDto), GoalScored>()
                .ForMember(dest => dest.GoalId, opt => opt.MapFrom(src => src.goalId));
            CreateMap<UpdateGoalScoredDto, GoalScored>();

            CreateMap<OwnGoal, OwnGoalResponseDto>();
            CreateMap<CreateOwnGoalDto, OwnGoal>();
            CreateMap<(int goalId, CreateOwnGoalDto), OwnGoal>()
                .ForMember(dest => dest.GoalId, opt => opt.MapFrom(src => src.goalId));
            CreateMap<UpdateOwnGoalDto, OwnGoal>();

            CreateMap<Assist, AssistResponseDto>();
            CreateMap<CreateAssistDto, Assist>();
            CreateMap<(int goalId, CreateAssistDto), Assist>()
                .ForMember(dest => dest.GoalId, opt => opt.MapFrom(src => src.goalId));
            CreateMap<UpdateAssistDto, Assist>();
        }
    }
}
