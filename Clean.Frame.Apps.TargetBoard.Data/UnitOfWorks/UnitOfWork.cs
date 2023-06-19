using Clean.Frame.Apps.TargetBoard.Core.Repositories;
using Clean.Frame.Apps.TargetBoard.Core.UnitOfWorks;
using Clean.Frame.Apps.TargetBoard.Data.Repositories;
using System.Threading.Tasks;

namespace Clean.Frame.Apps.TargetBoard.Data.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        private MainBoardRepository _mainBoardRepository;
        private UnitRepository _unitRepository;
        private TargetRepository _targetRepository;
        private AspectRepository _aspectRepository;


        public IMainBoardRepository MainBoardRepository => _mainBoardRepository = _mainBoardRepository ?? new MainBoardRepository(_context); 
        public IUnitRepository UnitRepository => _unitRepository = _unitRepository ?? new UnitRepository(_context); 
        public ITargetRepository TargetRepository => _targetRepository = _targetRepository ?? new TargetRepository(_context); 
        public IAspectRepository AspectRepository => _aspectRepository = _aspectRepository ?? new AspectRepository(_context); 


        public UnitOfWork(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
