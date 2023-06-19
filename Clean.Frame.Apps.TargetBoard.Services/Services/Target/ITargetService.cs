using Clean.Frame.Apps.TargetBoard.Core.Entities;
using Clean.Frame.Apps.TargetBoard.Services.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Frame.Apps.TargetBoard.Core.Services
{
    public interface ITargetService : IService<Target>
    {
        Task<TargetDto> GetByIdAsync(int id);
        Task<List<TargetDto>> GetAllByAspectIdAsync(int aspectId);
        Task<bool> CheckHasTargetRelationForDelete(int id);
    }
}

    

