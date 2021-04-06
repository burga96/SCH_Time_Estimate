using Core.Domain.Entities;
using Core.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Domain.RepositoryInterfaces
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : class
    {
        Task Insert(TEntity entity);

        Task InsertMany(ICollection<TEntity> entities);

        Task<bool> Update(TEntity entity);

        Task<bool> Delete(TEntity entity);

        Task<bool> DeleteWithIncludes(Expression<Func<TEntity, bool>> identity, params Expression<Func<TEntity, object>>[] includes);

        Task<IReadOnlyCollection<TEntity>> GetAllList();

        Task<IReadOnlyCollection<TEntity>> GetAllWithIncludesAsList(params Expression<Func<TEntity, object>>[] includes);

        Task<IReadOnlyCollection<TEntity>> GetFilteredList(Expression<Func<TEntity, bool>> filter);

        Task<IReadOnlyCollection<TEntity>> SearchByWithIncludes(Expression<Func<TEntity, bool>> searchBy, params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity> GetById(object id);

        Task<ResultsAndTotalCount<TEntity>> GetResultsAndTotalCountAsync(
            Expression<Func<TEntity, bool>> filterExpression = null,
            OrderBySettings<TEntity> orderBySettings = null,
            int skip = 0,
            int? take = null,
            params Expression<Func<TEntity, object>>[] includes
        );

        Task<TEntity> GetFirstOrDefault(Expression<Func<TEntity, bool>> filter);

        Task<TEntity> FindFirstOrDefaultByWithIncludes(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity> GetFirstWithIncludes(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includePropertyExpressions);
    }
}