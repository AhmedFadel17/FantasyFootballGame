using FantasyFootballGame.DataAccess.Data;
using FantasyFootballGame.Domain.Models.Actions;

namespace FantasyFootballGame.DataAccess.Repositories.Actions.Saves
{
    public class SavesRepository : BaseRepository<Save>, ISavesRepository
    {
        public SavesRepository(AppDbContext context) : base(context)
        {
        }
    }
}
