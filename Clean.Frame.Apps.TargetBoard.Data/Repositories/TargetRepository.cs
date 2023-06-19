using Microsoft.EntityFrameworkCore;
using Clean.Frame.Apps.TargetBoard.Core.Entities;
using Clean.Frame.Apps.TargetBoard.Core.Repositories;
using System.Threading.Tasks;

namespace Clean.Frame.Apps.TargetBoard.Data.Repositories
{
    public class TargetRepository : Repository<Target>, ITargetRepository
    {
        private AppDbContext _appDbContext { get => _context as AppDbContext; }

        public TargetRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<Target> GetAspectTargetsById(int id)
        {
            return await _appDbContext.Targets.Include(x => x.Aspect).Where(x=>x.Id==id).FirstOrDefaultAsync();

        }
    }

}
