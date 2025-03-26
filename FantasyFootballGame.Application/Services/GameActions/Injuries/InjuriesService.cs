using AutoMapper;
using FantasyFootballGame.Application.DTOs.GameActions.Injuries;
using FantasyFootballGame.Application.Interfaces.GameActions.Injuries;
using FantasyFootballGame.DataAccess.Repositories.Actions.Injuries;
using FantasyFootballGame.Domain.Models.Actions;
using FluentValidation;

namespace FantasyFootballGame.Application.Services.GameActions.Injuries
{
    public class InjuriesService : IInjuriesService
    {
        private readonly IInjuriesRepository _repo;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateInjuryDto> _createValidator;
        private readonly IValidator<UpdateInjuryDto> _updateValidator;

        public InjuriesService(
            IInjuriesRepository injuriesRepository, 
            IMapper mapper,
            IValidator<CreateInjuryDto> createValidator,
            IValidator<UpdateInjuryDto> updateValidator)
        {
            _mapper = mapper;
            _repo = injuriesRepository;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }
        
        public async Task<InjuryResponseDto> Create(CreateInjuryDto dto)
        {
            await _createValidator.ValidateAndThrowAsync(dto);
            var injury = _mapper.Map<Injury>(dto);
            await _repo.Create(injury);
            await _repo.Save();
            return _mapper.Map<InjuryResponseDto>(injury);
        }

        public async Task Delete(int id)
        {
            var injury = await _repo.GetById(id);
            if (injury == null) throw new KeyNotFoundException("Injury not found");
            _repo.Delete(injury);
            await _repo.Save();
        }

        public async Task<InjuryResponseDto> GetById(int id)
        {
            var injury = await _repo.GetById(id);
            if (injury == null) throw new KeyNotFoundException("Injury not found");
            return _mapper.Map<InjuryResponseDto>(injury);
        }

        public async Task<InjuryResponseDto> Update(int id, UpdateInjuryDto dto)
        {
            await _updateValidator.ValidateAndThrowAsync(dto);
            var injury = await _repo.GetById(id);
            if (injury == null) throw new KeyNotFoundException("Injury not found");
            _mapper.Map(dto, injury);
            _repo.Update(injury);
            await _repo.Save();
            return _mapper.Map<InjuryResponseDto>(injury);
        }
    }
}
