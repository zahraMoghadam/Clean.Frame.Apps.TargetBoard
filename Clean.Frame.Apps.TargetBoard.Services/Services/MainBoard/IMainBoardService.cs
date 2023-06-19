using Clean.Frame.Apps.TargetBoard.Core.Entities;
using Clean.Frame.Apps.TargetBoard.Services.Services.Dtos;
using System.Threading.Tasks;

namespace Clean.Frame.Apps.TargetBoard.Core.Services
{
    public interface IMainBoardService:IService<MainBoard>
    {
        Task<MainBoardDto> GetByIdAsync(int categoryId);
        Task<List<MainBoardDto>> GetAllByDateAsync(int year, int month);
        Task<List<MainBoardDto>> GetAllByUnitAsync();
        Task<bool> CheckHasMainBoardAspectsForDelete(int id);
    }
}
