using Clean.Frame.Apps.TargetBoard.Core.Repositories;
using System.Threading.Tasks;

namespace Clean.Frame.Apps.TargetBoard.Core.UnitOfWorks
{
    public interface IUnitOfWork
    {
        IMainBoardRepository MainBoardRepository { get; }
        IUnitRepository UnitRepository { get; }
        ITargetRepository TargetRepository { get; }
        IAspectRepository AspectRepository { get; }

        Task CommitAsync();

        void Commit();
    }
}
