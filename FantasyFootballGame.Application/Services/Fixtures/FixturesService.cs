﻿using AutoMapper;
using FantasyFootballGame.Application.DTOs.Common;
using FantasyFootballGame.Application.DTOs.Fixtures;
using FantasyFootballGame.Application.Interfaces.Fixtures;
using FantasyFootballGame.DataAccess.Repositories.Fixtures;
using FantasyFootballGame.Domain.Models;
using FluentValidation;

namespace FantasyFootballGame.Application.Services.Fixtures
{
    public class FixturesService : IFixturesService
    {
        private readonly IFixturesRepository _fixturesRepo;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateFixtureDto> _createValidator;
        private readonly IValidator<UpdateFixtureDto> _updateValidator;

        public FixturesService(
            IFixturesRepository fixturesRepo,
            IMapper mapper,
            IValidator<CreateFixtureDto> createValidator,
            IValidator<UpdateFixtureDto> updateValidator)
        {
            _fixturesRepo = fixturesRepo;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<List<FixtureResponseDto>> All()
        {
            var fixtures = await _fixturesRepo.GetAll();
            return _mapper.Map<List<FixtureResponseDto>>(fixtures);
        }

        public async Task<FixtureResponseDto> Create(CreateFixtureDto dto)
        {
            await _createValidator.ValidateAndThrowAsync(dto);
            var fixture = _mapper.Map<Fixture>(dto);
            await _fixturesRepo.Create(fixture);
            await _fixturesRepo.Save();
            return _mapper.Map<FixtureResponseDto>(fixture);
        }

        public async Task Delete(int id)
        {
            var fixture = await _fixturesRepo.GetById(id);
            if (fixture == null)
                throw new Exception($"Fixture with id {id} not found");
            _fixturesRepo.Delete(fixture);
            await _fixturesRepo.Save();
        }

        public async Task<FixtureResponseDto> GetById(int id)
        {
            var fixture = await _fixturesRepo.GetById(id);
            if (fixture == null)
                throw new Exception($"Fixture with id {id} not found");
            return _mapper.Map<FixtureResponseDto>(fixture);
        }

        public async Task<FixtureResponseDto> Update(int id, UpdateFixtureDto dto)
        {
            await _updateValidator.ValidateAndThrowAsync(dto);
            var fixture = await _fixturesRepo.GetById(id);
            if (fixture == null)
                throw new Exception($"Fixture with id {id} not found");
            _mapper.Map(dto, fixture);
            _fixturesRepo.Update(fixture);
            await _fixturesRepo.Save();
            return _mapper.Map<FixtureResponseDto>(fixture);
        }

        public async Task AddGoal(int fixtureId,int teamId)
        {
            var fixture = await _fixturesRepo.GetById(fixtureId);
            if (fixture == null) throw new KeyNotFoundException("Fixture not found");
            if (fixture.HomeTeamId == teamId) fixture.HomeTeamScore += 1;
            if (fixture.AwayTeamId == teamId) fixture.AwayTeamScore += 1;
            _fixturesRepo.Update(fixture);
            await _fixturesRepo.Save();
        }

        public async Task CancelGoal(int fixtureId, int teamId)
        {
            var fixture = await _fixturesRepo.GetById(fixtureId);
            if (fixture == null) throw new KeyNotFoundException("Fixture not found");
            if (fixture.HomeTeamId == teamId) fixture.HomeTeamScore -= 1;
            if (fixture.AwayTeamId == teamId) fixture.AwayTeamScore -= 1;
            _fixturesRepo.Update(fixture);
            await _fixturesRepo.Save();
        }

        public async Task<PaginationDto<FixtureResponseDto>> AllWithPagination(
            int page, int pageSize, int? teamId, int? gameweekId, int? playerId, DateOnly? date)
        {
            var fixtures = await _fixturesRepo.GetAllWithPagination(page, pageSize, teamId, gameweekId,playerId,date);
            var paginationSource = new PaginationSource<Fixture>(fixtures.Item1.ToList(), page, pageSize, fixtures.Item2);
            return _mapper.Map<PaginationDto<FixtureResponseDto>>(paginationSource);
        }
    }
}
