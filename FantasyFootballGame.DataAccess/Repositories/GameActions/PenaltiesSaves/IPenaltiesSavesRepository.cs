using FantasyFootballGame.Domain.Models.Actions.Penalties;

namespace FantasyFootballGame.DataAccess.Repositories.Actions.PenaltiesSaves
{
    public interface IPenaltiesSavesRepository : IBaseRepository<PenaltySave>
    {
        Task<PenaltySave> GetPenaltySaveByPenaltyId(int penaltyId);
    }
}
