using System.Linq.Expressions;
using WebDataModel.BaseClass;

namespace WebRepository.Interface
{
    public interface IRepository<TEnty> 
        where TEnty : class
    {
        TEnty Get(Expression<Func<TEnty, bool>> expression);

        IQueryable<TEnty> GetAll(Paginationpage<TEnty> paginationpage);

        IQueryable<TEnty> GetAll(Expression<Func<TEnty, bool>> expression);

        Task<bool> CheckWord(Expression<Func<TEnty, bool>> expression);

        bool Add(TEnty entity);

        bool AddRange(IEnumerable<TEnty> entities);

        bool Remove(TEnty entity);

        bool RemoveRange(IEnumerable<TEnty> entities);

        bool Update(TEnty entity);

        bool UpdateRange(IEnumerable<TEnty> entities);

        Task<TEnty> GetAsync(Expression<Func<TEnty, bool>> expression, CancellationToken cancellationToken = default);

        Task<IEnumerable<TEnty>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<IEnumerable<TEnty>> GetAllAsync(Expression<Func<TEnty, bool>> expression, CancellationToken cancellationToken = default);

        Task<bool> AddAsync(TEnty entity, CancellationToken cancellationToken = default);

        Task<bool> AddRangeAsync(IEnumerable<TEnty> entities, CancellationToken cancellationToken = default);
    }
}