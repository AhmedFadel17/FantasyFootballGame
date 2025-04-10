using AutoMapper;
using FantasyFootballGame.Application.DTOs.Players;
using FantasyFootballGame.Application.Interfaces.Players;
using FantasyFootballGame.DataAccess.Repositories.Players;
using FantasyFootballGame.Domain.Enums;
using FantasyFootballGame.Domain.Models;
using FantasyFootballGame.Application.DTOs.Common;
using FluentValidation;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace FantasyFootballGame.Application.Services.Players
{
    public class PlayersService : IPlayersService
    {
        private readonly IPlayersRepository _repo;
        private readonly IMapper _mapper;
        private readonly IValidator<CreatePlayerDto> _createPlayerValidator;
        private readonly IValidator<UpdatePlayerDto> _updatePlayerValidator;
        public PlayersService(IPlayersRepository repository,IMapper mapper,IValidator<CreatePlayerDto> createPlayerValidator,IValidator<UpdatePlayerDto> updatePlayerValidator)
        {
            _mapper = mapper;
            _repo = repository;
            _createPlayerValidator = createPlayerValidator;
            _updatePlayerValidator = updatePlayerValidator;
        }
        public async Task<List<PlayerResponseDto>> All()
        {
            var players= await _repo.GetAll();
            return _mapper.Map<List<PlayerResponseDto>>(players);
        }

        public async Task<PaginationDto<PlayerResponseDto>> AllWithPaginationAndFilters
            (int page, int pageSize,int? teamId,int? shirtNumber, string? name,PlayerStatus? status, PlayerPosition? position, double? minPrice, double? maxPrice)
        {
            var players= await _repo.GetAllWithPaginationAndFilters(page,pageSize,teamId,shirtNumber,name,status,position,minPrice,maxPrice);
            var paginationSource = new PaginationSource<Player>(players.Item1.ToList(), page, pageSize,players.Item2);
            return _mapper.Map<PaginationDto<PlayerResponseDto>>(paginationSource);
        }

        public async Task<PlayerResponseDto> Create(CreatePlayerDto dto)
        {
            var validationResult = await _createPlayerValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
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
            var validationResult = await _updatePlayerValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            var player = await _repo.GetById(id);
            if (player == null) throw new KeyNotFoundException("Player is not found");
            _mapper.Map(dto, player);
            _repo.Update(player);
            await _repo.Save();
            return _mapper.Map<PlayerResponseDto>(player);
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
