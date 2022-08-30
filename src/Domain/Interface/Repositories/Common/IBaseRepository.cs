using Entity.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Interface.Repositories.Common
{
    public interface IBaseRepository<T> where T : class, IEntity
    {
        DbContext UnitOfWork { get; }
        Task<int> SaveContextAsync();

        int SaveContext();
        IDbContextTransaction BeginTransaction();
        void Attach(T entity);

        IList<T> GetData(Expression<Func<T, bool>> filter = null,
                         Func<IQueryable<T>,
                         IOrderedQueryable<T>> orderBy = null,
                         List<Expression<Func<T, object>>> includeProperties = null,
                         bool noTrack = false);

        T GetData(long entityId,
                  List<Expression<Func<T, object>>> includeProperties = null,
                  bool noTrack = false);


        Task<IList<T>> GetDataAsync(Expression<Func<T, bool>> filter = null,
                                    Func<IQueryable<T>,
                                    IOrderedQueryable<T>> orderBy = null,
                                    List<Expression<Func<T, object>>> includeProperties = null,
                                    bool noTrack = false);

        Task<T> GetDataAsync(long entityId,
                             List<Expression<Func<T, object>>> includeProperties = null,
                             bool noTrack = false);

        void Insert(T entity);

        void Insert(IEnumerable<T> entitis);

        void Update(T entity);

        int Delete(long entityId);

        int Delete(T entity);
    }
}
