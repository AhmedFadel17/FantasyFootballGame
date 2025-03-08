using FantasyFootballGame.DataAccess.Data;
using FantasyFootballGame.Domain.Models.Actions;

namespace FantasyFootballGame.DataAccess.Repositories.Transfers
{
    public class TransfersRepository : BaseRepository<Transfer>, ITransfersRepository
    {
        public TransfersRepository(AppDbContext context) : base(context)
        {
        }
    }
}
