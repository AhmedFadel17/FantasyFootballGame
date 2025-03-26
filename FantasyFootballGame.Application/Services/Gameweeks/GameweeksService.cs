using AutoMapper;
using FantasyFootballGame.Application.DTOs.Gameweeks;
using FantasyFootballGame.Application.Interfaces.Gameweeks;
using FantasyFootballGame.DataAccess.Repositories.Gameweeks;
using FantasyFootballGame.Domain.Models;
using FluentValidation;

namespace FantasyFootballGame.Application.Services.Gameweeks
{
    public class GameweeksService : IGameweeksService
    {
        private readonly IGameweeksRepository _gameweeksRepo;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateGameweekDto> _createValidator;
        private readonly IValidator<UpdateGameweekDto> _updateValidator;

        public GameweeksService(
            IGameweeksRepository gameweeksRepo,
            IMapper mapper,
            IValidator<CreateGameweekDto> createValidator,
            IValidator<UpdateGameweekDto> updateValidator)
        {
            _gameweeksRepo = gameweeksRepo;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<List<GameweekResponseDto>> All()
        {
            var gameweeks = await _gameweeksRepo.GetAll();
            return _mapper.Map<List<GameweekResponseDto>>(gameweeks);
        }

        public async Task<GameweekResponseDto> Create(CreateGameweekDto dto)
        {
            await _createValidator.ValidateAndThrowAsync(dto);
            var gameweek = _mapper.Map<Gameweek>(dto);
            await _gameweeksRepo.Create(gameweek);
            await _gameweeksRepo.Save();
            return _mapper.Map<GameweekResponseDto>(gameweek);
        }

        public async Task Delete(int id)
        {
            var gameweek = await _gameweeksRepo.GetById(id);
            if (gameweek == null)
                throw new Exception($"Gameweek with id {id} not found");
            _gameweeksRepo.Delete(gameweek);
            await _gameweeksRepo.Save();
        }

        public async Task<GameweekResponseDto> GetById(int id)
        {
            var gameweek = await _gameweeksRepo.GetById(id);
            if (gameweek == null)
                throw new Exception($"Gameweek with id {id} not found");
            return _mapper.Map<GameweekResponseDto>(gameweek);
        }

        public async Task<GameweekResponseDto> Update(int id, UpdateGameweekDto dto)
        {
            await _updateValidator.ValidateAndThrowAsync(dto);
            var gameweek = await _gameweeksRepo.GetById(id);
            if (gameweek == null)
                throw new Exception($"Gameweek with id {id} not found");
            _mapper.Map(dto, gameweek);
            _gameweeksRepo.Update(gameweek);
            await _gameweeksRepo.Save();
            return _mapper.Map<GameweekResponseDto>(gameweek);
        }

        public async Task<Gameweek> GetCurrentGameweek()
        {
            var gameweek = await _gameweeksRepo.GetCurrentGameweek();
            if (gameweek == null)
                throw new Exception("No active gameweek found");
            return gameweek;
        }

        public async Task<IEnumerable<Gameweek>> GetAll()
        {
            return await _gameweeksRepo.GetAll();
        }
    }
}

