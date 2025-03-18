using AutoMapper;
using FantasyFootballGame.Application.DTOs.GameActions.Injuries;
using FantasyFootballGame.Application.Interfaces.GameActions.Injuries;
using FantasyFootballGame.DataAccess.Repositories.Actions.Injuries;
using FantasyFootballGame.Domain.Models.Actions;

namespace FantasyFootballGame.Application.Services.GameActions.Injuries
{
    public class InjuriesService : IInjuriesService
    {
        private readonly IInjuriesRepository _repo;
        private readonly IMapper _mapper;
        public InjuriesService(IInjuriesRepository injuriesRepository, IMapper mapper)
        {
            _mapper = mapper;
            _repo = injuriesRepository;
        }
        
        public async Task<InjuryResponseDto> Create(CreateInjuryDto dto)
        {
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
            var injury = await _repo.GetById(id);
            if (injury == null) throw new KeyNotFoundException("Injury not found");
            var updatedInjury = _mapper.Map<Injury>(injury);
            _repo.Update(updatedInjury);
            await _repo.Save();
            return _mapper.Map<InjuryResponseDto>(updatedInjury);
        }
    }
}
