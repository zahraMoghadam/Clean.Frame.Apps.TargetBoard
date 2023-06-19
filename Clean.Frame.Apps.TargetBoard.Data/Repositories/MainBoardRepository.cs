using Microsoft.EntityFrameworkCore;
using Clean.Frame.Apps.TargetBoard.Core.Entities;
using Clean.Frame.Apps.TargetBoard.Core.Repositories;
using System.Threading.Tasks;
using System.Linq.Expressions;


namespace Clean.Frame.Apps.TargetBoard.Data.Repositories
{
    public class MainBoardRepository : Repository<MainBoard>, IMainBoardRepository
    {
        private AppDbContext _appDbContext { get => _context as AppDbContext; }

        public MainBoardRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<List<MainBoard>> GetAllByUnitAsync() {
            return await _appDbContext.MainBoards.Include(x => x.Unit).ToListAsync();
        }
        public async Task<List<MainBoard>> GetAllRelationAsync(int year, int month)
        {
            Expression<Func<MainBoard, bool>> query = null;
            Expression predicate = null;

            if ((year!=0) && (month!=0))
            {
                query = query = x => x.Month.Equals(month) && x.Year.Equals(year);
            }
            predicate = query;
            if (predicate is not null)
            {
                return await _appDbContext.MainBoards.Include(x => x.Aspects).ThenInclude(x => x.Targets).Where((Expression<Func<MainBoard, bool>>)predicate).ToListAsync();
            }
            else
            {
                return await _appDbContext.MainBoards.Include(x => x.Aspects).ThenInclude(x => x.Targets).ToListAsync();
            }
            //Expression<Func<MainBoard, bool>> queryYear = x => x.Year == year.Value;
            //Expression<Func<MainBoard, bool>> queryMonth = null;
            //Expression predicate = null;

            //var parameter = Expression.Parameter(typeof(MainBoard), "i");

            //if (year.HasValue)
            //{
            //    queryYear = queryYear = x => x.Year.Equals(year.Value);
            //    if (queryYear is not null)
            //    {
            //        predicate = queryYear;
            //    }
            //}
            //if (month.HasValue)
            //{
            //    queryMonth = queryMonth = x => x.Month.Equals(month.Value);
            //    if (queryYear is null)
            //    {
            //        predicate = queryMonth;
            //    }
            //    else
            //    {

            //        // predicate = Expression.AndAlso(queryYear.Body, queryMonth);
            //        predicate = Expression.Lambda<Func<MainBoard, bool>>(Expression.AndAlso(queryYear, queryMonth), queryMonth.Parameters);
            //    }
            //    //query = query is null ? x => x.Month == month : query.AndAlso(x => x.Month == month);
            //}
            //if (predicate is not null)
            //{

            //    return await _appDbContext.MainBoards.Include(x => x.Aspects).ThenInclude(x => x.Targets).Where((Expression<Func<MainBoard, bool>>)predicate).ToListAsync();
            //}
            //else
            //{ return await _appDbContext.MainBoards.Include(x => x.Aspects).ThenInclude(x => x.Targets).ToListAsync(); }
        }
        public async Task<List<MainBoard>> GetMainBoardAspectsById(int id)
        {
            return await _appDbContext.MainBoards.Include(x => x.Aspects).Where(x => x.Id == id).ToListAsync();

        }
    }
}
