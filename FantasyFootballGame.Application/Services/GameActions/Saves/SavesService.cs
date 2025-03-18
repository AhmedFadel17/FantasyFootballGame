using AutoMapper;
using FantasyFootballGame.Application.DTOs.GameActions.Saves;
using FantasyFootballGame.Application.Interfaces.GameActions.Saves;
using FantasyFootballGame.DataAccess.Repositories.Actions.Saves;
using FantasyFootballGame.Domain.Models.Actions;

namespace FantasyFootballGame.Application.Services.GameActions.Saves
{
    public class SavesService : ISavesService
    {
        private readonly ISavesRepository _repo;
        private readonly IMapper _mapper;
        public SavesService(ISavesRepository savesRepository,IMapper mapper)
        {
            _mapper = mapper;
            _repo = savesRepository;
        }
        public async Task<SaveResponseDto> Create(CreateSaveDto dto)
        {
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
            var save = await _repo.GetById(id);
            if (save == null) throw new KeyNotFoundException("Save not found");
            var updatedSave = _mapper.Map<Save>(save);
            _repo.Update(updatedSave);
            await _repo.Save();
            return _mapper.Map<SaveResponseDto>(updatedSave);
        }
    }
}
