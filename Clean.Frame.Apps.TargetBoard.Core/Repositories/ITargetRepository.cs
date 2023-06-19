using Clean.Frame.Apps.TargetBoard.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Frame.Apps.TargetBoard.Core.Repositories
{
    public interface ITargetRepository : IRepository<Target>
    {
        Task<Target> GetByIdAsync(int id);
        Task<Target> GetAspectTargetsById(int id);
    }
}
