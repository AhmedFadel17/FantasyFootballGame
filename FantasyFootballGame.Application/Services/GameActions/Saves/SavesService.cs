using AutoMapper;
using FantasyFootballGame.Application.DTOs.GameActions.Saves;
using FantasyFootballGame.Application.Interfaces.GameActions.Saves;
using FantasyFootballGame.DataAccess.Repositories.Actions.Saves;
using FantasyFootballGame.Domain.Models.Actions;
using FluentValidation;

namespace FantasyFootballGame.Application.Services.GameActions.Saves
{
    public class SavesService : ISavesService
    {
        private readonly ISavesRepository _repo;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateSaveDto> _createValidator;
        private readonly IValidator<UpdateSaveDto> _updateValidator;

        public SavesService(
            ISavesRepository savesRepository,
            IMapper mapper,
            IValidator<CreateSaveDto> createValidator,
            IValidator<UpdateSaveDto> updateValidator)
        {
            _mapper = mapper;
            _repo = savesRepository;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<SaveResponseDto> Create(CreateSaveDto dto)
        {
            await _createValidator.ValidateAndThrowAsync(dto);
            var save = _mapper.Map<Save>(dto);
            await _repo.Create(save);
            await _repo.Save();
            return _mapper.Map<SaveResponseDto>(save);
        }

        public async Task Delete(int id)
        {
            var save = await _repo.GetById(id);
            if (save == null) throw new KeyNotFoundException("Save not found");
            _repo.Delete(save);
            await _repo.Save();
        }

        public async Task<SaveResponseDto> GetById(int id)
        {
            var save = await _repo.GetById(id);
            if (save == null) throw new KeyNotFoundException("Save not found");
            return _mapper.Map<SaveResponseDto>(save);
        }

        public async Task<SaveResponseDto> Update(int id, UpdateSaveDto dto)
        {
            await _updateValidator.ValidateAndThrowAsync(dto);
            var save = await _repo.GetById(id);
            if (save == null) throw new KeyNotFoundException("Save not found");
            _mapper.Map(dto, save);
            _repo.Update(save);
            await _repo.Save();
            return _mapper.Map<SaveResponseDto>(save);
        }
    }
}
