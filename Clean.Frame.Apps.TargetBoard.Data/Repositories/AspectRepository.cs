using Microsoft.EntityFrameworkCore;
using Clean.Frame.Apps.TargetBoard.Core.Entities;
using Clean.Frame.Apps.TargetBoard.Core.Repositories;
using System.Threading.Tasks;

namespace Clean.Frame.Apps.TargetBoard.Data.Repositories
{
    public class AspectRepository : Repository<Aspect>, IAspectRepository
    {
        private AppDbContext _appDbContext { get => _context as AppDbContext; }

        public AspectRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<List<Aspect>> GetByMainBoardIdAsync(int id)
        {
            return await _appDbContext.Aspects.Include(x => x.Targets).Where(x => x.MainBoardId == id).ToListAsync();
        }
        public async Task<List<Aspect>> GetByMainBoardIdWithoutRelationAsync(int id)
        {
            return await _appDbContext.Aspects.Where(x => x.MainBoardId == id).ToListAsync();
        }
        public async Task<List<Aspect>> GetAspectTargetsById(int id)
        {
            return await _appDbContext.Aspects.Include(x => x.Targets).Where(x => x.Id == id).ToListAsync();

        }
    }
}
