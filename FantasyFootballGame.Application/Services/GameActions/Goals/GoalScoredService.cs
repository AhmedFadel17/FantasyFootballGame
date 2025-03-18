using AutoMapper;
using FantasyFootballGame.Application.DTOs.GameActions.Goals.GoalsScored;
using FantasyFootballGame.Application.Interfaces.GameActions.Goals;
using FantasyFootballGame.DataAccess.Repositories.Actions.GoalsScored;
using FantasyFootballGame.DataAccess.Repositories.Actions.OwnGoals;
using FantasyFootballGame.Domain.Models.Actions.Goals;

namespace FantasyFootballGame.Application.Services.GameActions.Goals
{
    public class GoalScoredService : IGoalScoredService
    {
        private readonly IGoalsScoredRepository _repo;
        private readonly IOwnGoalsRepository _ownGoalsRepository;
        private readonly IMapper _mapper;
        public GoalScoredService(IGoalsScoredRepository repository, IOwnGoalsRepository ownGoalsRepository, IMapper mapper)
        {
            _repo = repository;
            _mapper = mapper;
            _ownGoalsRepository = ownGoalsRepository;
        }
        public async Task<GoalScoredResponseDto> Create(CreateGoalScoredDto dto)
        {
            bool goalHasOwnGoal = await _ownGoalsRepository.CheckGoalHasOwnGoal(dto.GoalId);
            bool goalHasScored = await _repo.CheckGoalHasScored(dto.GoalId);
            if (goalHasScored || goalHasOwnGoal) throw new InvalidOperationException("This Goal has already scorer");
            var goalScored = _mapper.Map<GoalScored>(dto);
            await _repo.Create(goalScored);
            await _repo.Save();
            return _mapper.Map<GoalScoredResponseDto>(goalScored);
        }

        public async Task<GoalScoredResponseDto> GetById(int id)
        {
            var goalScored = await _repo.GetById(id);
            if (goalScored == null) throw new KeyNotFoundException("Goal Scored not found");
            return _mapper.Map<GoalScoredResponseDto>(goalScored);
        }

        public async Task<GoalScoredResponseDto> Update(int id, UpdateGoalScoredDto dto)
        {
            var goalScored = await _repo.GetById(id);
            if (goalScored == null) throw new KeyNotFoundException("Goal Scored not found");
            var updatedGoalScored = _mapper.Map<GoalScored>(dto);
            _repo.Update(updatedGoalScored);
            await _repo.Save();
            return _mapper.Map<GoalScoredResponseDto>(updatedGoalScored);
        }
    }
}
