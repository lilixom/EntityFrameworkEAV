using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TAP.FileService.Infrastructure
{
    public interface IRepository<TEntity, TId> where TEntity : class, IEntity<TId>
    {
        // Methods
        IQueryable<TEntity> DbSet();

        void Add(TEntity entity);

        TEntity Get(TId id);

        void Remove(TEntity entity);

        void Save(TEntity entity);

        void Update(TEntity entity);

        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate = null);

        IUnitOfWork UnitOfWork
        {
            get;
        }

        #region 批量移除 4EF6

        //void Remove(Expression<Func<TEntity, bool>> filterExpression);

        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="query"></param>
        //void Remove(IQueryable<TEntity> query);

        ///// <summary>
        ///// 移除
        ///// </summary>
        ///// <param name="entities"></param>
        ///// <returns></returns>
        //IEnumerable<TEntity> RemoveRange(IEnumerable<TEntity> entities);

        #endregion 批量移除 4EF6

        #region 批量更新4Ef6

        //void Update(Expression<Func<TEntity, TEntity>> updateExpression);

        //void Update(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, TEntity>> updateExpression);

        //void Update(IQueryable<TEntity> query, Expression<Func<TEntity, TEntity>> updateExpression);

        #endregion 批量更新4Ef6
    }
}