using AutoMapper;
using FantasyFootballGame.Application.DTOs.GameActions.Penalties;
using FantasyFootballGame.Application.DTOs.GameActions.Penalties.PenaltiesMissed;
using FantasyFootballGame.Application.DTOs.GameActions.Penalties.PenaltiesSaves;
using FantasyFootballGame.Application.Interfaces.GameActions.Penalties;
using FantasyFootballGame.DataAccess.Repositories.Actions.Penalties;
using FantasyFootballGame.DataAccess.Repositories.Actions.PenaltiesMissed;
using FantasyFootballGame.DataAccess.Repositories.Actions.PenaltiesSaves;
using FantasyFootballGame.Domain.Models.Actions.Penalties;
using FluentValidation;

namespace FantasyFootballGame.Application.Services.GameActions.Penalties
{
    public class PenaltiesService : IPenaltiesService
    {
        private readonly IPenaltiesRepository _penaltiesRepo;
        private readonly IPenaltiesMissedRepository _penaltiesMissedRepo;
        private readonly IPenaltiesSavesRepository _penaltiesSavesRepo;
        private readonly IMapper _mapper;
        private readonly IPenaltiesMissedService _penaltiesMissedService;
        private readonly IPenaltiesSavesService _penaltiesSavesService;
        private readonly IValidator<CreatePenaltyDto> _createValidator;
        private readonly IValidator<UpdatePenaltyDto> _updateValidator;

        public PenaltiesService(
            IPenaltiesRepository penaltiesRepo,
            IPenaltiesMissedRepository penaltiesMissedRepo,
            IPenaltiesSavesRepository penaltiesSavesRepo,
            IPenaltiesMissedService penaltiesMissedService,
            IPenaltiesSavesService penaltiesSavesService,
            IMapper mapper,
            IValidator<CreatePenaltyDto> createValidator,
            IValidator<UpdatePenaltyDto> updateValidator)
        {
            _penaltiesRepo = penaltiesRepo;
            _penaltiesMissedRepo = penaltiesMissedRepo;
            _penaltiesSavesRepo = penaltiesSavesRepo;
            _penaltiesMissedService = penaltiesMissedService;
            _penaltiesSavesService = penaltiesSavesService;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task Delete(int id)
        {
            var penalty = await _penaltiesRepo.GetById(id);
            if (penalty == null)
                throw new Exception($"Penalty with id {id} not found");
            _penaltiesRepo.Delete(penalty);
            await _penaltiesRepo.Save();
        }

        public async Task<PenaltyResponseDto> GetById(int id)
        {
            var penalty = await _penaltiesRepo.GetById(id);
            if (penalty == null)
                throw new Exception($"Penalty with id {id} not found");
            return _mapper.Map<PenaltyResponseDto>(penalty);
        }

        public async Task<PenaltyResponseDto> Create(CreatePenaltyDto dto)
        {
            await _createValidator.ValidateAndThrowAsync(dto);
            var penalty = _mapper.Map<Penalty>(dto);
            await _penaltiesRepo.Create(penalty);
            await _penaltiesRepo.Save();
            if(!penalty.IsScored)
            {
                if (dto.MissPlayerId.HasValue) 
                {
                    var missDto = new CreatePenaltyMissedDto
                    {
                        PenaltyId = penalty.Id,
                        PlayerId = dto.MissPlayerId.Value,
                    };
                    await _penaltiesMissedService.Create(missDto);
                }
                if (dto.SavePlayerId.HasValue)
                {
                    var saveDto = new CreatePenaltySaveDto
                    {
                        PenaltyId = penalty.Id,
                        PlayerId = dto.SavePlayerId.Value,
                    };
                    await _penaltiesSavesService.Create(saveDto);
                }
            }
            return _mapper.Map<PenaltyResponseDto>(penalty);
        }

        public async Task<PenaltyResponseDto> Update(int id, UpdatePenaltyDto dto)
        {
            await _updateValidator.ValidateAndThrowAsync(dto);
            var penalty = await _penaltiesRepo.GetById(id);
            if (penalty == null)
                throw new Exception($"Penalty with id {id} not found");
            _mapper.Map(dto, penalty);
            _penaltiesRepo.Update(penalty);
            await _penaltiesRepo.Save();
            return _mapper.Map<PenaltyResponseDto>(penalty);
        }
    }
}
