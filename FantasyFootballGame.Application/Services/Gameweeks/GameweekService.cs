using AutoMapper;
using FantasyFootballGame.Application.DTOs.Gameweeks;
using FantasyFootballGame.Application.Interfaces.Gameweeks;
using FantasyFootballGame.DataAccess.Repositories.Gameweeks;
using FantasyFootballGame.Domain.Models;

namespace FantasyFootballGame.Application.Services.Gameweeks
{
    public class GameweekService : IGameweeksService
    {
        private readonly IGameweeksRepository _repo;
        private readonly IMapper _mapper;
        public GameweekService(IGameweeksRepository repository,IMapper mapper)
        {
            _mapper = mapper;
            _repo = repository;
        }
        public async Task<List<GameweekResponseDto>> All()
        {
            var gameweeks= await _repo.GetAll();
            return _mapper.Map<List<GameweekResponseDto>>(gameweeks);
        }

        public async Task<GameweekResponseDto> Create(CreateGameweekDto dto)
        {
            var gameweek = _mapper.Map<Gameweek>(dto);
            await _repo.Create(gameweek);
            await _repo.Save();
            return _mapper.Map<GameweekResponseDto>(gameweek);
        }

        public async Task Delete(int id)
        {
            var gameweek=await _repo.GetById(id);
            if (gameweek == null) throw new KeyNotFoundException("Gameweek not found");
            _repo.Delete(gameweek);
            await _repo.Save();
        }

        public async Task<GameweekResponseDto> GetById(int id)
        {
            var gameweek = await _repo.GetById(id);
            if (gameweek == null) throw new KeyNotFoundException("Gameweek not found");
            return _mapper.Map<GameweekResponseDto>(gameweek);
        }

        public async Task<GameweekResponseDto> Update(int id, UpdateGameweekDto dto)
        {
            var gameweek = await _repo.GetById(id);
            if (gameweek == null) throw new KeyNotFoundException("Gameweek not found");
            var updatedGameweek=_mapper.Map<Gameweek>(dto);
            _repo.Update(updatedGameweek);
            await _repo.Save();
            return _mapper.Map<GameweekResponseDto>(updatedGameweek);
        }
    }
}
