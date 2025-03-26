using AutoMapper;
using FantasyFootballGame.Application.DTOs.GameActions.Penalties;
using FantasyFootballGame.Application.DTOs.GameActions.Penalties.PenaltiesMissed;
using FantasyFootballGame.Application.DTOs.GameActions.Penalties.PenaltiesSaves;
using FantasyFootballGame.Application.Interfaces.GameActions.Penalties;
using FantasyFootballGame.DataAccess.Repositories.Actions.PenaltiesSaves;
using FantasyFootballGame.Domain.Models.Actions.Penalties;
using FluentValidation;

namespace FantasyFootballGame.Application.Services.GameActions.Penalties
{
    public class PenaltiesSavesService : IPenaltiesSavesService
    {
        private readonly IPenaltiesSavesRepository _repo;
        private readonly IMapper _mapper;
        private readonly IValidator<CreatePenaltySaveDto> _createValidator;
        private readonly IValidator<UpdatePenaltySaveDto> _updateValidator;

        public PenaltiesSavesService(
            IPenaltiesSavesRepository penaltiesSavesRepository,
            IMapper mapper,
            IValidator<CreatePenaltySaveDto> createValidator,
            IValidator<UpdatePenaltySaveDto> updateValidator)
        {
            _mapper = mapper;
            _repo = penaltiesSavesRepository;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<PenaltySaveResponseDto> Create(CreatePenaltySaveDto dto)
        {
            await _createValidator.ValidateAndThrowAsync(dto);
            var penalty = _mapper.Map<PenaltySave>(dto);
            await _repo.Create(penalty);
            await _repo.Save();
            return _mapper.Map<PenaltySaveResponseDto>(penalty);
        }

        public async Task Delete(int id)
        {
            var penalty = await _repo.GetById(id);
            if (penalty == null) throw new KeyNotFoundException("Penalty save not found");
            _repo.Delete(penalty);
            await _repo.Save();
        }

        public async Task<PenaltySaveResponseDto> GetById(int id)
        {
            var penalty = await _repo.GetById(id);
            if (penalty == null) throw new KeyNotFoundException("Penalty save not found");
            return _mapper.Map<PenaltySaveResponseDto>(penalty);
        }

        public async Task<PenaltySaveResponseDto> Update(int id, UpdatePenaltySaveDto dto)
        {
            await _updateValidator.ValidateAndThrowAsync(dto);
            var penalty = await _repo.GetById(id);
            if (penalty == null) throw new KeyNotFoundException("Penalty save not found");
            _mapper.Map(dto, penalty);
            _repo.Update(penalty);
            await _repo.Save();
            return _mapper.Map<PenaltySaveResponseDto>(penalty);
        }
    }
}
