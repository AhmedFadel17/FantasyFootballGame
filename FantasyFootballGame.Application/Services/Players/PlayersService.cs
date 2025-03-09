using AutoMapper;
using FantasyFootballGame.Application.DTOs.Players;
using FantasyFootballGame.Application.Interfaces.Players;
using FantasyFootballGame.DataAccess.Repositories.Players;
using FantasyFootballGame.Domain.Models;

namespace FantasyFootballGame.Application.Services.Players
{
    public class PlayersService : IPlayersService
    {
        private readonly IPlayersRepository _repo;
        private readonly IMapper _mapper;
        public PlayersService(IPlayersRepository repository,IMapper mapper)
        {
            _mapper = mapper;
            _repo = repository;
        }
        public async Task<List<PlayerResponseDto>> All()
        {
            var teams= await _repo.GetAll();
            return _mapper.Map<List<PlayerResponseDto>>(teams);
        }

        public async Task<PlayerResponseDto> Create(CreatePlayerDto dto)
        {
            var player = _mapper.Map<Player>(dto);
            await _repo.Create(player);
            await _repo.Save();
            return _mapper.Map<PlayerResponseDto>(player);
        }

        public async Task<PlayerResponseDto> GetById(int id)
        {
            var player = await _repo.GetById(id);
            if (player == null) throw new KeyNotFoundException("Player is not found");
            return _mapper.Map<PlayerResponseDto>(player);
        }

        public async Task<List<PlayerResponseDto>> GetByName(string name)
        {
            var players = await _repo.GetByName(name);
            return _mapper.Map<List<PlayerResponseDto>>(players);
        }

        public async Task<List<PlayerResponseDto>> GetByPrice(double min, double max)
        {
            var players = await _repo.GetByPrice(min,max);
            return _mapper.Map<List<PlayerResponseDto>>(players);
        }

        public async Task<PlayerResponseDto> Update(int id, UpdatePlayerDto dto)
        {
            var player = await _repo.GetById(id);
            if (player == null) throw new KeyNotFoundException("Player is not found");
            var updatedPlayer = _mapper.Map<Player>(player);
            _repo.Update(updatedPlayer);
            await _repo.Save();
            return _mapper.Map<PlayerResponseDto>(updatedPlayer);
        }

        public async Task Delete(int id)
        {
            var player = await _repo.GetById(id);
            if (player == null) throw new KeyNotFoundException("Player is not found");
            _repo.Delete(player);
            await _repo.Save();
        }
    }
}
