using FantasyFootballGame.Domain.Models.Actions.Penalties;

namespace FantasyFootballGame.DataAccess.Repositories.Actions.PenaltiesMissed
{
    public interface IPenaltiesMissedRepository : IBaseRepository<PenaltyMiss>
    {
        Task<PenaltyMiss> GetMissedPenaltyByPenaltyId(int penaltyId);   
    }
}
