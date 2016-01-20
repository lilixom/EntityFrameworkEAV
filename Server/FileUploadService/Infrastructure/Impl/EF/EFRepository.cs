using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TAP.FileService.Infrastructure.Impl.EF
{
    public class EFRepository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class,IEntity<TId>
    {
        public EFRepository(IUnitOfWork unitOfWork)
        {
            this.DbContext = (DbContext)unitOfWork;
        }

        public IUnitOfWork UnitOfWork
        {
            get { return this.DbContext as IUnitOfWork; }
        }

        public void Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            GetSet().Add(entity);
        }

        private DbSet<TEntity> GetSet()
        {
            return this.DbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> DbSet()
        {
            return GetSet();
        }

        public TEntity Get(TId id)
        {
            if (string.IsNullOrEmpty(id.ToString()))
            {
                throw new ArgumentNullException("id");
            }
            return GetSet().Find(new object[] { id });
        }

        public void Remove(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            GetSet().Remove(entity);
        }

        public void Save(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            DbEntityEntry<TEntity> entry = this.DbContext.Entry<TEntity>(entity);
            DbSet<TEntity> set = GetSet();
            if (entry.State == EntityState.Detached)
            {
                set.Add(entity);
            }
        }

        public void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            DbSet<TEntity> set = GetSet();
            if (set.Local.All<TEntity>(t => !t.Id.Equals(entity.Id)))
            {
                set.Attach(entity);
                this.DbContext.Entry<TEntity>(entity).State = EntityState.Modified;
            }
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate = null)
        {
            return ((predicate == null) ? GetSet() : GetSet().Where<TEntity>(predicate));
        }

        // Properties
        protected DbContext DbContext { get; private set; }
    }
}