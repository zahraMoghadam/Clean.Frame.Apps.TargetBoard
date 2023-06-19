using System.Linq.Expressions;
using Clean.Frame.Apps.TargetBoard.Core.Repositories;
using Clean.Frame.Apps.TargetBoard.Core.Services;
using Clean.Frame.Apps.TargetBoard.Core.UnitOfWorks;

namespace Clean.Frame.Apps.TargetBoard.Services.Services
{
    public class Service<TEntity> : IService<TEntity> where TEntity : class
    {
        public readonly IUnitOfWork UnitOfWork; 
        private readonly IRepository<TEntity> _repository;

        public Service(IUnitOfWork unitOfWork,IRepository<TEntity> repository)
        {
            UnitOfWork = unitOfWork;
            _repository = repository;
        }


        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _repository.AddAsync(entity);

            await UnitOfWork.CommitAsync();

            return entity;
        }

        public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            var addRangeAsync = entities.ToList();
            await _repository.AddRangeAsync(addRangeAsync);

            await UnitOfWork.CommitAsync();

            return addRangeAsync;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public void Remove(TEntity entity)
        {
            _repository.Remove(entity);
            UnitOfWork.Commit();
        }


        public async Task RemoveAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null)
            {
                _repository.Remove(entity);
                await UnitOfWork.CommitAsync();
            }
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _repository.RemoveRange(entities);

            UnitOfWork.Commit();
        }

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _repository.SingleOrDefaultAsync(predicate);
        }

        public TEntity Update(TEntity entity)
        {
           TEntity updateEntity=  _repository.Update(entity);

            UnitOfWork.Commit();

            return updateEntity;
        }

        public async Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return await _repository.Where(predicate);
        }
    }
}
