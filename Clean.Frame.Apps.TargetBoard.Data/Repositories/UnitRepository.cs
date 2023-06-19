using Microsoft.EntityFrameworkCore;
using Clean.Frame.Apps.TargetBoard.Core.Entities;
using Clean.Frame.Apps.TargetBoard.Core.Repositories;
using System.Threading.Tasks;

namespace Clean.Frame.Apps.TargetBoard.Data.Repositories
{
    public class UnitRepository : Repository<Unit>, IUnitRepository
    {
        private AppDbContext _appDbContext { get => _context as AppDbContext; }

        public UnitRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Unit> GetUnitMainBoardsById(int id)
        {
            return await _appDbContext.Units.Include(x => x.MainBoards).Where(x=>x.Id==id).FirstOrDefaultAsync();

        }
    }
}
