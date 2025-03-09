using AutoMapper;
using FantasyFootballGame.Application.DTOs.Fixtures;
using FantasyFootballGame.Application.Interfaces.Fixtures;
using FantasyFootballGame.DataAccess.Repositories.Fixtures;
using FantasyFootballGame.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyFootballGame.Application.Services.Fixtures
{
    public class FixturesService : IFixturesService
    {
        private readonly IFixturesRepository _repo;
        private readonly IMapper _mapper;
        public FixturesService(IFixturesRepository repository,IMapper mapper)
        {
            _mapper = mapper;
            _repo = repository; 
        }

        public async Task<List<FixtureResponseDto>> All()
        {
            var fixtures = await _repo.GetAll();    
            return _mapper.Map<List<FixtureResponseDto>>(fixtures);
        }

        public async Task<FixtureResponseDto> Create(CreateFixtureDto dto)
        {
            var fixture = _mapper.Map<Fixture>(dto);
            await _repo.Create(fixture);
            return _mapper.Map<FixtureResponseDto>(fixture);
        }

        public async Task Delete(int id)
        {
            var fixture = await _repo.GetById(id);
            if(fixture == null) throw new KeyNotFoundException("Fixture not found");
            _repo.Delete(fixture);
            await _repo.Save();
        }

        public async Task<FixtureResponseDto> GetById(int id)
        {
            var fixture = await _repo.GetById(id);
            if (fixture == null) throw new KeyNotFoundException("Fixture not found");
            return _mapper.Map<FixtureResponseDto>(fixture);
        }

        public async Task<FixtureResponseDto> Update(int id, UpdateFixtureDto dto)
        {
            var fixture = await _repo.GetById(id);
            if (fixture == null) throw new KeyNotFoundException("Fixture not found");
            var updatedFixture = _mapper.Map<Fixture>(dto);
            _repo.Update(updatedFixture);
            await _repo.Save();
            return _mapper.Map<FixtureResponseDto>(updatedFixture);
        }
    }
}
