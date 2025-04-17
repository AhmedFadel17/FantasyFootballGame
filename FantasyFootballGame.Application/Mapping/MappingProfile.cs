using AutoMapper;
using FantasyFootballGame.Application.DTOs.Common;
using FantasyFootballGame.Application.DTOs.FantasyTeamPlayers;
using FantasyFootballGame.Application.DTOs.FantasyTeams;
using FantasyFootballGame.Application.DTOs.Fixtures;
using FantasyFootballGame.Application.DTOs.GameActions.BonusPoints;
using FantasyFootballGame.Application.DTOs.GameActions.BonusPoints.IndvidualBonusPoints;
using FantasyFootballGame.Application.DTOs.GameActions.Cards;
using FantasyFootballGame.Application.DTOs.GameActions.Goals;
using FantasyFootballGame.Application.DTOs.GameActions.Goals.Assists;
using FantasyFootballGame.Application.DTOs.GameActions.Goals.GoalsScored;
using FantasyFootballGame.Application.DTOs.GameActions.Goals.OwnGoals;
using FantasyFootballGame.Application.DTOs.GameActions.Injuries;
using FantasyFootballGame.Application.DTOs.GameActions.Penalties;
using FantasyFootballGame.Application.DTOs.GameActions.Penalties.PenaltiesMissed;
using FantasyFootballGame.Application.DTOs.GameActions.Penalties.PenaltiesSaves;
using FantasyFootballGame.Application.DTOs.GameActions.Saves;
using FantasyFootballGame.Application.DTOs.Gameweeks;
using FantasyFootballGame.Application.DTOs.Players;
using FantasyFootballGame.Application.DTOs.Teams;
using FantasyFootballGame.Application.DTOs.Transfers;
using FantasyFootballGame.Domain.Models;
using FantasyFootballGame.Domain.Models.Actions;
using FantasyFootballGame.Domain.Models.Actions.Goals;
using FantasyFootballGame.Domain.Models.Actions.Penalties;
using FantasyFootballGame.Domain.Enums;
using System;

namespace FantasyFootballGame.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap(typeof(PaginationSource<>), typeof(PaginationDto<>))
                .ConvertUsing(typeof(PaginationConverter<,>));


            CreateMap<Gameweek, GameweekResponseDto>();
            CreateMap<CreateGameweekDto, Gameweek>();
            CreateMap<UpdateGameweekDto, Gameweek>();

            CreateMap<Fixture, FixtureResponseDto>();
            CreateMap<CreateFixtureDto, Fixture>();
            CreateMap<UpdateFixtureDto, Fixture>();

            CreateMap<Team,TeamResponseDto>();
            CreateMap<CreateTeamDto, Team>();
            CreateMap<UpdateTeamDto, Team>();

            CreateMap<Player, PlayerResponseDto>()
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position.ToString()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<CreatePlayerDto, Player>()
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => Enum.Parse<PlayerPosition>(src.Position, false)))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => PlayerStatus.Available));

            CreateMap<UpdatePlayerDto, Player>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Position, opt => opt.Condition(src => src.Position != null))
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => Enum.Parse<PlayerPosition>(src.Position, false)))
                .ForMember(dest => dest.Price, opt => opt.Condition(src => src.Price != null && src.Price.HasValue))
                .ForMember(dest => dest.ShirtNumber, opt => opt.Condition(src => src.ShirtNumber != null && src.ShirtNumber.HasValue))
                .ForMember(dest => dest.TeamId, opt => opt.Condition(src => src.TeamId != null && src.TeamId.HasValue))
                .ForMember(dest => dest.Name, opt => opt.Condition(src => src.Name != null))
                .ForMember(dest => dest.FullName, opt => opt.Condition(src => src.FullName != null))
                .ForMember(dest => dest.Status, opt => opt.Condition(src => src.Status != null))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<PlayerStatus>(src.Status, false)));

            CreateMap<FantasyTeam, FantasyTeamResponseDto>();
            CreateMap<(int userId,double squadValue,double inTheBank, CreateFantasyTeamDto), FantasyTeam>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.userId))
                .ForMember(dest => dest.SquadValue, opt => opt.MapFrom(src => src.squadValue))
                .ForMember(dest => dest.InTheBank, opt => opt.MapFrom(src => src.inTheBank));
            CreateMap<UpdateFantasyTeamDto, FantasyTeam>();

            CreateMap<FantasyTeamPlayer, FantasyTeamPlayerResponseDto>();
            CreateMap<(int teamId,CreateFantasyTeamPlayerDto), FantasyTeamPlayer>()
                .ForMember(dest => dest.FantasyTeamId,opt =>opt.MapFrom(src => src.teamId));
            CreateMap<UpdateFantasyTeamPlayerDto, FantasyTeamPlayer>();

            CreateMap<(int fantasyTeamId,int gameweekId, CreateTransferDto), Transfer>()
                .ForMember(dest => dest.FantasyTeamId, opt => opt.MapFrom(src => src.fantasyTeamId))
                .ForMember(dest => dest.GameweekId, opt => opt.MapFrom(src => src.gameweekId));


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

            CreateMap<Card,CardResponseDto>();
            CreateMap<CreateCardDto,Card>();
            CreateMap<UpdateCardDto, Card>();

            CreateMap<Injury, InjuryResponseDto>();
            CreateMap<CreateInjuryDto, Injury>();
            CreateMap<UpdateInjuryDto, Injury>();

            CreateMap<Save, SaveResponseDto>();
            CreateMap<CreateSaveDto, Save>();
            CreateMap<UpdateSaveDto, Save>();

            CreateMap<PenaltySave, PenaltySaveResponseDto>();
            CreateMap<CreatePenaltySaveDto, PenaltySave>();
            CreateMap<UpdatePenaltySaveDto, PenaltySave>();

            CreateMap<PenaltyMiss, PenaltyMissedResponseDto>();
            CreateMap<CreatePenaltyMissedDto, PenaltyMiss>();
            CreateMap<UpdatePenaltyMissedDto, PenaltyMiss>();

            CreateMap<Penalty, PenaltyResponseDto>();
            CreateMap<CreatePenaltyDto, Penalty>();
            CreateMap<UpdatePenaltyDto, Penalty>();

            CreateMap<CreateBonusPointDto, Bonus>();
            CreateMap<Bonus, BonusPointResponseDto>();
            CreateMap<CreateBonusPointsDto, BonusPointsResponseDto>()
                .ForMember(dest => dest.BonusPoints, opt => opt.Ignore());

        }
    }
}
