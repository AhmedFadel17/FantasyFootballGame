using AutoMapper;
using FantasyFootballGame.Application.DTOs.Goals;
using FantasyFootballGame.DataAccess.Repositories.Actions.Assists;
using FantasyFootballGame.DataAccess.Repositories.Actions.Goals;
using FantasyFootballGame.DataAccess.Repositories.Actions.GoalsScored;
using FantasyFootballGame.DataAccess.Repositories.Actions.OwnGoals;
using FantasyFootballGame.Domain.Models.Actions;

namespace FantasyFootballGame.Application.Interfaces.Goals
{
    public class GoalsService : IGoalsService
    {
        private readonly IGoalsRepository _goalsRepo;
        private readonly IGoalsScoredRepository _goalsScoredRepo;
        private readonly IAssistsRepository _assistsRepo;
        private readonly IOwnGoalsRepository _ownGoalsRepo;
        private readonly IMapper _mapper;
        public GoalsService(
            IGoalsRepository goalsRepository,
            IOwnGoalsRepository ownGoalsRepository,
            IAssistsRepository assistsRepository,
            IGoalsScoredRepository goalsScoredRepo,
            IMapper mapper)
        {
            _mapper = mapper;
            _goalsRepo = goalsRepository;
            _goalsScoredRepo = goalsScoredRepo;
            _assistsRepo = assistsRepository;
            _ownGoalsRepo = ownGoalsRepository;
        }

        public async Task<GoalResponseDto> GetById(int id)
        {
            var goal = await _goalsRepo.GetById(id);
            if (goal == null) throw new KeyNotFoundException("Goal not found");
            return _mapper.Map<GoalResponseDto>(goal);
        }

        public async Task<List<GoalResponseDto>> GetByFixture(int fixtureId)
        {
            var goals = await _goalsRepo.GetByFixture(fixtureId);
            return _mapper.Map<List<GoalResponseDto>>(goals);
        }

        public async Task<List<GoalResponseDto>> GetByGameweek(int gameweekId)
        {
            var goals = await _goalsRepo.GetByGameweek(gameweekId);
            return _mapper.Map<List<GoalResponseDto>>(goals);
        }

        public async Task<List<GoalResponseDto>> GetByPlayer(int playerId)
        {
            var goals = await _goalsRepo.GetByPlayer(playerId);
            return _mapper.Map<List<GoalResponseDto>>(goals);
        }

        public async Task<List<GoalResponseDto>> GetByTeam(int teamId)
        {
            var goals = await _goalsRepo.GetByTeam(teamId);
            return _mapper.Map<List<GoalResponseDto>>(goals);
        }

        public async Task<GoalResponseDto> Update(int id, UpdateGoalDto dto)
        {
            var goal = await _goalsRepo.GetById(id);
            if (goal == null) throw new KeyNotFoundException("Goal not found");
            var updatedGoal = _mapper.Map<Goal>(dto);
            _goalsRepo.Update(updatedGoal);
            await _goalsRepo.Save();
            return _mapper.Map<GoalResponseDto>(updatedGoal);
        }

        public async Task<GoalResponseDto> Create(CreateGoalDto dto)
        {
            var goal= _mapper.Map<Goal>(dto);
            await _goalsRepo.Create(goal);
            await _goalsRepo.Save();
            if(dto.GoalScored != null)
            {
                var goalScored = _mapper.Map<GoalScored>((goal.Id,dto));
                await _goalsScoredRepo.Create(goalScored);
                await _goalsScoredRepo.Save();
            }
            else if(dto.OwnGoal != null)
            {
                var ownGoal = _mapper.Map<OwnGoal>((goal.Id,dto));
                await _ownGoalsRepo.Create(ownGoal);
                await _ownGoalsRepo.Save();
            }

            if (dto.Assist != null) 
            {
                var assist = _mapper.Map<Assist>((goal.Id,dto));
                await _assistsRepo.Create(assist);
                await _assistsRepo.Save();
            }
            
            return _mapper.Map<GoalResponseDto>(goal);
        }

        public async Task Delete(int id)
        {
            var goal = await _goalsRepo.GetById(id);
            if (goal == null) throw new KeyNotFoundException("Goal not found");
            _goalsRepo.Delete(goal);
            await _goalsRepo.Save();
        }
    }
}
