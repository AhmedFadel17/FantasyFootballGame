using AutoMapper;
using FantasyFootballGame.Application.DTOs.Goals.OwnGoals;
using FantasyFootballGame.DataAccess.Repositories.Actions.GoalsScored;
using FantasyFootballGame.DataAccess.Repositories.Actions.OwnGoals;
using FantasyFootballGame.Domain.Models.Actions;

namespace FantasyFootballGame.Application.Interfaces.Goals
{
    public class OwnGoalsService : IOwnGoalsService
    {
        private readonly IOwnGoalsRepository _repo;
        private readonly IGoalsScoredRepository _goalsScoredRepository;

        private readonly IMapper _mapper;
        public OwnGoalsService(IOwnGoalsRepository repository,IGoalsScoredRepository goalsScoredRepository, IMapper mapper)
        {
            _repo = repository;
            _mapper = mapper;
            _goalsScoredRepository = goalsScoredRepository;
        }

        public async Task<OwnGoalResponseDto> Create(CreateOwnGoalDto dto)
        {
            bool goalHasOwnGoal = await _repo.CheckGoalHasOwnGoal(dto.GoalId);
            bool goalHasScored = await _goalsScoredRepository.CheckGoalHasScored(dto.GoalId);
            if (goalHasScored || goalHasOwnGoal) throw new InvalidOperationException("This Goal has already scorer");
            var ownGoal = _mapper.Map<OwnGoal>(dto);
            await _repo.Create(ownGoal);
            await _repo.Save();
            return _mapper.Map<OwnGoalResponseDto>(ownGoal);
        }

        public async Task<OwnGoalResponseDto> GetById(int id)
        {
            var ownGoal = await _repo.GetById(id);
            if (ownGoal == null) throw new KeyNotFoundException("Own goal not found");
            return _mapper.Map<OwnGoalResponseDto>(ownGoal);
        }

        public async Task<OwnGoalResponseDto> Update(int id, UpdateOwnGoalDto dto)
        {
            var ownGoal = await _repo.GetById(id);
            if (ownGoal == null) throw new KeyNotFoundException("Own goal not found");
            var updatedOwnGoal = _mapper.Map<OwnGoal>(ownGoal);
            _repo.Update(updatedOwnGoal);
            await _repo.Save();
            return _mapper.Map<OwnGoalResponseDto>(updatedOwnGoal);
        }
    }
}
