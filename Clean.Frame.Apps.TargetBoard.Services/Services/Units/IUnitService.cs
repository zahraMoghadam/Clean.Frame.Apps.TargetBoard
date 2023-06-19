using Clean.Frame.Apps.TargetBoard.Core.Entities;
using Clean.Frame.Apps.TargetBoard.Core.Services;
using Clean.Frame.Apps.TargetBoard.Services.Services.Dtos;

namespace Clean.Frame.Apps.TargetBoard.Services.Services
{
    public interface IUnitService : IService<Unit>
    {
        new Task<UnitDto?> GetByIdAsync(int id);
        Task<bool> CheckHasUnitRelationForDelete(int id);

    }
}