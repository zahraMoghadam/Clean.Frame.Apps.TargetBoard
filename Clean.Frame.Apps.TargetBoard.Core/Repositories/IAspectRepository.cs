using Clean.Frame.Apps.TargetBoard.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Frame.Apps.TargetBoard.Core.Repositories
{
    public interface IAspectRepository : IRepository<Aspect>
    {
        Task<Aspect> GetByIdAsync(int id);
        Task<List<Aspect>> GetByMainBoardIdAsync(int id);
        Task<List<Aspect>> GetByMainBoardIdWithoutRelationAsync(int id);
        Task<List<Aspect>> GetAspectTargetsById(int id);
    }
}
