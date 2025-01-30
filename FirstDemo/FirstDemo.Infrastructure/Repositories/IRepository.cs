using System.Linq.Expressions;

namespace FirstDemo.Infrastructure.Repositories
{
    public interface IRepository<TEntity, TKey>
    {
        void Add(TEntity entity);
        void Edit(TEntity entityToUpdate);
        IList<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", bool isTrackingOff = false);
        (IList<TEntity> data, int total, int totalDisplay) Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", int pageIndex = 1, int pageSize = 10, bool isTrackingOff = false);
        IList<TEntity> Get(Expression<Func<TEntity, bool>> filter, string includeProperties = "");
        IList<TEntity> GetAll();
        TEntity GetById(TKey id);
        int GetCount(Expression<Func<TEntity, bool>> filter = null);
        IList<TEntity> GetDynamic(Expression<Func<TEntity, bool>> filter = null, string orderBy = null, string includeProperties = "", bool isTrackingOff = false);
        (IList<TEntity> data, int total, int totalDisplay) GetDynamic(Expression<Func<TEntity, bool>> filter = null, string orderBy = null, string includeProperties = "", int pageIndex = 1, int pageSize = 10, bool isTrackingOff = false);
        void Remove(Expression<Func<TEntity, bool>> filter);
        void Remove(TEntity entityToDelete);
        void Remove(TKey id);
    }
}