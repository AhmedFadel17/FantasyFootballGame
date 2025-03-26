using AutoMapper;
using FantasyFootballGame.Application.DTOs.GameActions.Goals.Assists;
using FantasyFootballGame.Application.Interfaces.GameActions.Goals;
using FantasyFootballGame.DataAccess.Repositories.Actions.Assists;
using FantasyFootballGame.Domain.Models.Actions.Goals;
using FluentValidation;

namespace FantasyFootballGame.Application.Services.GameActions.Goals
{
    public class AssistsService : IAssistsService
    {
        private readonly IAssistsRepository _repo;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateAssistDto> _createValidator;
        private readonly IValidator<UpdateAssistDto> _updateValidator;

        public AssistsService(
            IAssistsRepository repository, 
            IMapper mapper,
            IValidator<CreateAssistDto> createValidator,
            IValidator<UpdateAssistDto> updateValidator)
        {
            _mapper = mapper;
            _repo = repository;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<AssistResponseDto> Create(CreateAssistDto dto)
        {
            await _createValidator.ValidateAndThrowAsync(dto);
            bool goalHasAssits = await _repo.CheckGoalHasAssist(dto.GoalId);
            if (goalHasAssits) throw new InvalidOperationException("This Goal has already assist");
            var assist = _mapper.Map<Assist>(dto);
            await _repo.Create(assist);
            await _repo.Save();
            return _mapper.Map<AssistResponseDto>(assist);
        }

        public async Task Delete(int id)
        {
            var assist = await _repo.GetById(id);
            if (assist == null) throw new KeyNotFoundException("Assist not found");
            _repo.Delete(assist);
            await _repo.Save();
        }

        public async Task<AssistResponseDto> GetById(int id)
        {
            var assist = await _repo.GetById(id);
            if (assist == null) throw new KeyNotFoundException("Assist not found");
            return _mapper.Map<AssistResponseDto>(assist);
        }

        public async Task<AssistResponseDto> Update(int id, UpdateAssistDto dto)
        {
            await _updateValidator.ValidateAndThrowAsync(dto);
            var assist = await _repo.GetById(id);
            if (assist == null) throw new KeyNotFoundException("Assist not found");
            _mapper.Map(dto, assist);
            _repo.Update(assist);
            await _repo.Save();
            return _mapper.Map<AssistResponseDto>(assist);
        }
    }
}
