using AutoMapper;
using FantasyFootballGame.Application.DTOs.GameActions.Penalties.PenaltiesMissed;
using FantasyFootballGame.Application.Interfaces.GameActions.Penalties;
using FantasyFootballGame.DataAccess.Repositories.Actions.PenaltiesMissed;
using FantasyFootballGame.Domain.Models.Actions.Penalties;
using FluentValidation;

namespace FantasyFootballGame.Application.Services.GameActions.Penalties
{
    public class PenaltiesMissedService : IPenaltiesMissedService
    {
        private readonly IPenaltiesMissedRepository _repo;
        private readonly IMapper _mapper;
        private readonly IValidator<CreatePenaltyMissedDto> _createValidator;
        private readonly IValidator<UpdatePenaltyMissedDto> _updateValidator;

        public PenaltiesMissedService(
            IPenaltiesMissedRepository penaltiesMissedRepository,
            IMapper mapper,
            IValidator<CreatePenaltyMissedDto> createValidator,
            IValidator<UpdatePenaltyMissedDto> updateValidator)
        {
            _mapper = mapper;
            _repo = penaltiesMissedRepository;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<PenaltyMissedResponseDto> Create(CreatePenaltyMissedDto dto)
        {
            await _createValidator.ValidateAndThrowAsync(dto);
            var penalty = _mapper.Map<PenaltyMiss>(dto);
            await _repo.Create(penalty);
            await _repo.Save();
            return _mapper.Map<PenaltyMissedResponseDto>(penalty);
        }

        public async Task Delete(int id)
        {
            var penalty = await _repo.GetById(id);
            if (penalty == null) throw new KeyNotFoundException("Penalty miss not found");
            _repo.Delete(penalty);
            await _repo.Save();
        }

        public async Task<PenaltyMissedResponseDto> GetById(int id)
        {
            var penalty = await _repo.GetById(id);
            if (penalty == null) throw new KeyNotFoundException("Penalty miss not found");
            return _mapper.Map<PenaltyMissedResponseDto>(penalty);
        }

        public async Task<PenaltyMissedResponseDto> Update(int id, UpdatePenaltyMissedDto dto)
        {
            await _updateValidator.ValidateAndThrowAsync(dto);
            var penalty = await _repo.GetById(id);
            if (penalty == null) throw new KeyNotFoundException("Penalty miss not found");
            _mapper.Map(dto, penalty);
            _repo.Update(penalty);
            await _repo.Save();
            return _mapper.Map<PenaltyMissedResponseDto>(penalty);
        }
    }
}
