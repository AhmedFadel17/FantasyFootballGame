using AutoMapper;
using FantasyFootballGame.Application.DTOs.GameActions.Goals.OwnGoals;
using FantasyFootballGame.Application.Interfaces.GameActions.Goals;
using FantasyFootballGame.DataAccess.Repositories.Actions.GoalsScored;
using FantasyFootballGame.DataAccess.Repositories.Actions.OwnGoals;
using FantasyFootballGame.Domain.Models.Actions.Goals;
using FluentValidation;

namespace FantasyFootballGame.Application.Services.GameActions.Goals
{
    public class OwnGoalsService : IOwnGoalsService
    {
        private readonly IOwnGoalsRepository _repo;
        private readonly IGoalsScoredRepository _goalsScoredRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateOwnGoalDto> _createValidator;
        private readonly IValidator<UpdateOwnGoalDto> _updateValidator;

        public OwnGoalsService(
            IOwnGoalsRepository repository,
            IGoalsScoredRepository goalsScoredRepository,
            IMapper mapper,
            IValidator<CreateOwnGoalDto> createValidator,
            IValidator<UpdateOwnGoalDto> updateValidator)
        {
            _repo = repository;
            _mapper = mapper;
            _goalsScoredRepository = goalsScoredRepository;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<OwnGoalResponseDto> Create(CreateOwnGoalDto dto)
        {
            await _createValidator.ValidateAndThrowAsync(dto);
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
            await _updateValidator.ValidateAndThrowAsync(dto);
            var ownGoal = await _repo.GetById(id);
            if (ownGoal == null) throw new KeyNotFoundException("Own goal not found");
            _mapper.Map(dto, ownGoal);
            _repo.Update(ownGoal);
            await _repo.Save();
            return _mapper.Map<OwnGoalResponseDto>(ownGoal);
        }
    }
}
