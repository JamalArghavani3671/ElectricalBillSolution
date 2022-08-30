using Entity.Entities.Common;
using Interface.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.EfCore.Repositories.Common
{
    public abstract class BaseRepository<T> : IBaseRepository<T>, IDisposable where T : class, IEntity
    {
        protected readonly DbContext _dbContext;
        protected DbSet<T> _DbSet;
        public DbContext UnitOfWork { get { return _dbContext; } }

        protected BaseRepository(DbContext context)
        {
            _dbContext = context;
            _DbSet = _dbContext.Set<T>();
        }

        public IList<T> GetData(Expression<Func<T, bool>> filter = null,
                                Func<IQueryable<T>,
                                IOrderedQueryable<T>> orderBy = null,
                                List<Expression<Func<T, object>>> includeProperties = null,
                                bool noTrack = false)
        {
            IQueryable<T> query = _DbSet;

            if (filter != null)
                query = query.Where(filter);

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                    query = query.Include(includeProperty);
            }

            if (orderBy != null)
                return (noTrack ? orderBy(query).AsNoTracking().ToList() : orderBy(query).ToList());
            else
                return (noTrack ? query.AsNoTracking().ToList() : query.ToList());
        }


        public T GetData(long entityId, List<Expression<Func<T, object>>> includeProperties = null, bool noTrack = false)
        {
            IQueryable<T> query = _DbSet;

            query = query.Where(a => a.Id == entityId);

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                    query = query.Include(includeProperty);
            }
            List<T> list = (noTrack ? query.AsNoTracking().ToList() : query.ToList());
            if (list.Count > 0)
                return list[0];
            return null;
        }

        public async Task<IList<T>> GetDataAsync(Expression<Func<T, bool>> filter = null,
                                           Func<IQueryable<T>,
                                           IOrderedQueryable<T>> orderBy = null,
                                           List<Expression<Func<T, object>>> includeProperties = null,
                                           bool noTrack = false)
        {
            IQueryable<T> query = _DbSet;

            if (filter != null)
                query = query.Where(filter);

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                    query = query.Include(includeProperty);
            }


            if (orderBy != null)
                return await (noTrack ? orderBy(query).AsNoTracking().ToListAsync() : orderBy(query).ToListAsync());
            else
                return await (noTrack ? query.AsNoTracking().ToListAsync() : query.ToListAsync());
        }


        public async Task<T> GetDataAsync(long entityId, List<Expression<Func<T, object>>> includeProperties = null, bool noTrack = false)
        {
            IQueryable<T> query = _DbSet;

            query = query.Where(a => a.Id == entityId);

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                    query = query.Include(includeProperty);
            }
            List<T> list = await (noTrack ? query.AsNoTracking().ToListAsync() : query.ToListAsync());
            if (list.Count > 0)
                return list[0];
            return null;
        }


        public virtual void Insert(T entity)
        {

            if (!ValaidateInsertion(entity))
                throw new Exception(InsertValidationMessage);

            _DbSet.Add(entity);
        }

        public virtual void Insert(IEnumerable<T> entitis)
        {
            foreach (var entry in entitis)
            {
                if (!ValaidateInsertion(entry))
                    throw new Exception(InsertValidationMessage);
            }
            foreach (var entry in entitis)
                Insert(entry);
        }

        public virtual void Update(T entity)
        {
            _DbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public int Delete(long entityId)
        {
            try
            {
                T entityToDelete = _DbSet.Find(entityId);

                if (!ValaidateDelete(entityToDelete))
                    throw new Exception(InsertValidationMessage);
                Delete(entityToDelete);
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public int Delete(T entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
                _DbSet.Attach(entity);

            if (!ValaidateDelete(entity))
                throw new Exception(InsertValidationMessage);
            _DbSet.Remove(entity);
            return 1;
        }


        public int SaveContext()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> SaveContextAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _dbContext.Database.BeginTransaction();
        }

        public void Attach(T entity)
        {
            _DbSet.Attach(entity);
        }

        #region Dispose

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;

            if (_dbContext == null) return;
            _dbContext.Dispose();
        }

        #endregion


        #region Validators
        protected string DeleteValidationMessage = "Delete Validation Failed";
        protected string InsertValidationMessage = "Insert Validation Failed";
        protected virtual bool ValaidateInsertion(T pEntity)
        {
            return true;
        }

        protected virtual bool ValaidateDelete(T pEntity)
        {
            return true;
        }
        #endregion
    }
}
