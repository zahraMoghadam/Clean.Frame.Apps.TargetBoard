using Clean.Frame.Apps.TargetBoard.Core.Entities;
using Clean.Frame.Apps.TargetBoard.Services.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Frame.Apps.TargetBoard.Core.Services
{
    public interface IAspectService : IService<Aspect>
    {
        Task<AspectDto> GetByIdAsync(int id);
        Task<List<AspectDto>> GetByMainBoardIdAsync(int mainBoardId);
        Task<List<AspectDto>> GetByMainBoardIdWithoutRelationAsync(int mainBoardId);
        Task<bool> CheckHasAspectRelationForDelete(int id);
    }
}