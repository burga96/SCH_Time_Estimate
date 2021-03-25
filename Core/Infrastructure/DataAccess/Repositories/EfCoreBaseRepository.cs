using Core.Domain.Entities;
using Core.Domain.RepositoryInterfaces;
using Core.Domain.ValueObjects;
using Core.Infrastructure.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Infrastructure.DataAccess.Repositories
{
    public abstract class EfCoreBaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        internal TimeEstimateDBContext _context;
        internal DbSet<TEntity> DbSet;

        public EfCoreBaseRepository(TimeEstimateDBContext context)
        {
            _context = context;
            DbSet = context.Set<TEntity>();
        }

        #region Insert

        public virtual async Task Insert(TEntity entity)
        {
            await DbSet.AddAsync(entity);
        }

        public virtual async Task InsertMany(ICollection<TEntity> entities)
        {
            await DbSet.AddRangeAsync(entities);
        }

        #endregion Insert

        #region Update

        public virtual async Task<bool> Update(TEntity entity)
        {
            try
            {
                DbSet.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;

                return await Task.FromResult(true);
            }
            catch (Exception)
            {
                return await Task.FromResult(false);
            }
        }

        #endregion Update

        #region Delete

        public virtual async Task<bool> DeleteWithIncludes(Expression<Func<TEntity, bool>> identity, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> results = DbSet.Where(identity);

            foreach (Expression<Func<TEntity, object>> includeExpression in includes)
                results = results.Include(includeExpression);
            try
            {
                DbSet.RemoveRange(results);
                return await Task.FromResult(true);
            }
            catch (Exception)
            {
                return await Task.FromResult(false);
            }
        }

        public virtual async Task<bool> Delete(TEntity entity)
        {
            DbSet.Remove(entity);
            return await Task.FromResult(true);
        }

        #endregion Delete

        #region GetList

        public virtual async Task<IReadOnlyCollection<TEntity>> GetAllList()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task<IReadOnlyCollection<TEntity>> GetAllWithIncludesAsList(params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> result = DbSet.Where(i => true);

            foreach (Expression<Func<TEntity, object>> includeExpression in includes)
                result = result.Include(includeExpression);

            return await result.ToListAsync();
        }

        public virtual async Task<IReadOnlyCollection<TEntity>> GetFilteredList(Expression<Func<TEntity, bool>> filter)
        {
            return await DbSet.Where(filter).ToListAsync();
        }

        #endregion GetList

        #region SearchBy

        public virtual async Task<IReadOnlyCollection<TEntity>> SearchByWithIncludes(Expression<Func<TEntity, bool>> searchBy, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> result = DbSet.Where(searchBy);

            foreach (Expression<Func<TEntity, object>> includeExpression in includes)
                result = result.Include(includeExpression);

            return await result.ToListAsync();
        }

        #endregion SearchBy

        public async Task<TEntity> GetById(object id)
        {
            return await DbSet.FindAsync(id);
        }

        #region GetFirstOrDefault

        /// <summary>
        /// Gets the first entity matching the filter, or its default value
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetFirstOrDefault(Expression<Func<TEntity, bool>> filter)
        {
            return await DbSet.FirstOrDefaultAsync(filter);
        }

        public virtual async Task<TEntity> FindFirstOrDefaultByWithIncludes(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> result = DbSet.Where(predicate);

            foreach (Expression<Func<TEntity, object>> includeExpression in includes)
                result = result.Include(includeExpression);

            return await result.FirstOrDefaultAsync();
        }

        #endregion GetFirstOrDefault

        public async Task<TEntity> GetFirstWithIncludes(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includePropertyExpressions)
        {
            return await AddMultipleIncludesToQuery(DbSet, includePropertyExpressions)
                .FirstOrDefaultAsync(filter);
        }

        protected IQueryable<TEntity> AddMultipleIncludesToQuery(IQueryable<TEntity> initialQuery, params Expression<Func<TEntity, object>>[] includePropertyExpressions)
        {
            IQueryable<TEntity> queryWithIncludes = includePropertyExpressions.Aggregate(initialQuery, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));
            return queryWithIncludes;
        }

        public async Task<ResultsAndTotalCount<TEntity>> GetResultsAndTotalCountAsync(
            Expression<Func<TEntity, bool>> filterExpression = null,
            OrderBySettings<TEntity> orderBySettings = null,
            int skip = 0,
            int? take = null,
            params Expression<Func<TEntity, object>>[] includes
        )
        {
            IQueryable<TEntity> query = DbSet.AsQueryable();

            if (filterExpression != null)
            {
                query = query.Where(filterExpression);
            }

            int? totalCount = (skip > 0 || take.HasValue)
                ? (int?)await query.CountAsync()
                : (int?)null;

            if (orderBySettings != null)
            {
                query = orderBySettings.IsAscending
                    ? query.OrderBy(orderBySettings.PropertySelector)
                    : query.OrderByDescending(orderBySettings.PropertySelector);
            }

            if (skip > 0)
            {
                query = query.Skip(skip);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            foreach (Expression<Func<TEntity, object>> includeExpression in includes)
            {
                query = query.Include(includeExpression);
            }

            ICollection<TEntity> entities = await query.ToListAsync();

            if (!totalCount.HasValue)
            {
                totalCount = entities.Count;
            }

            ResultsAndTotalCount<TEntity> resultsAndTotalCountDTO = new ResultsAndTotalCount<TEntity>(entities, totalCount.Value);

            return resultsAndTotalCountDTO;
        }

        public virtual void Dispose()
        {
            _context.Dispose();
        }
    }
}