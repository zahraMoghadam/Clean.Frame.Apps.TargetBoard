using Clean.Frame.Apps.TargetBoard.Core.Entities;
using System.Threading.Tasks;

namespace Clean.Frame.Apps.TargetBoard.Core.Repositories
{
    public interface IMainBoardRepository :IRepository<MainBoard>
    {
        Task<MainBoard> GetByIdAsync(int categoryId);
        Task<List<MainBoard>> GetAllRelationAsync(int year,int month);
        Task<List<MainBoard>> GetAllByUnitAsync();
        Task<List<MainBoard>> GetMainBoardAspectsById(int id); 
    }
}
