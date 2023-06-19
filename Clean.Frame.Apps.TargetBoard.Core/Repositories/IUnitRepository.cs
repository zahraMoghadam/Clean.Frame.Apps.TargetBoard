using Clean.Frame.Apps.TargetBoard.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Frame.Apps.TargetBoard.Core.Repositories
{
    public interface IUnitRepository : IRepository<Unit>
    {
        new Task<Unit?> GetByIdAsync(int id);
        Task<Unit> GetUnitMainBoardsById(int id);
    }
}
