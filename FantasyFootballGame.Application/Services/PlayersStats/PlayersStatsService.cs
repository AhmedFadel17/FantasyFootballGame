using AutoMapper;
using FantasyFootballGame.Application.DTOs.PlayersStats;
using FantasyFootballGame.Application.Interfaces.PlayersStats;
using FantasyFootballGame.DataAccess.Repositories.PlayersStats;
using FantasyFootballGame.Domain.Models;
using FluentValidation;

namespace FantasyFootballGame.Application.Services.PlayersStats
{
    public class PlayersStatsService : IPlayersStatsService
    {
        private readonly IPlayersStatsRepository _playersStatsRepo;
        private readonly IMapper _mapper;
        private readonly IValidator<CreatePlayerStatsDto> _createValidator;
        private readonly IValidator<UpdatePlayerStatsDto> _updateValidator;

        public PlayersStatsService(
            IPlayersStatsRepository playersStatsRepo,
            IMapper mapper,
            IValidator<CreatePlayerStatsDto> createValidator,
            IValidator<UpdatePlayerStatsDto> updateValidator)
        {
            _playersStatsRepo = playersStatsRepo;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<List<PlayerStatsResponseDto>> All()
        {
            var stats = await _playersStatsRepo.GetAll();
            return _mapper.Map<List<PlayerStatsResponseDto>>(stats);
        }

        public async Task<PlayerStatsResponseDto> Create(CreatePlayerStatsDto dto)
        {
            await _createValidator.ValidateAndThrowAsync(dto);
            var stats = _mapper.Map<PlayerGameweekForm>(dto);
            await _playersStatsRepo.Create(stats);
            await _playersStatsRepo.Save();
            return _mapper.Map<PlayerStatsResponseDto>(stats);
        }

        public async Task Delete(int id)
        {
            var stats = await _playersStatsRepo.GetById(id);
            if (stats == null)
                throw new Exception($"Player stats with id {id} not found");
            _playersStatsRepo.Delete(stats);
            await _playersStatsRepo.Save();
        }

        public async Task<PlayerStatsResponseDto> GetById(int id)
        {
            var stats = await _playersStatsRepo.GetById(id);
            if (stats == null)
                throw new Exception($"Player stats with id {id} not found");
            return _mapper.Map<PlayerStatsResponseDto>(stats);
        }

        public async Task<PlayerStatsResponseDto> Update(int id, UpdatePlayerStatsDto dto)
        {
            await _updateValidator.ValidateAndThrowAsync(dto);
            var stats = await _playersStatsRepo.GetById(id);
            if (stats == null)
                throw new Exception($"Player stats with id {id} not found");
            _mapper.Map(dto, stats);
            _playersStatsRepo.Update(stats);
            await _playersStatsRepo.Save();
            return _mapper.Map<PlayerStatsResponseDto>(stats);
        }
    }
} 