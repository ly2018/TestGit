using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Vic.Core.Data
{
    public interface IUnitOfWork
    {
        void BeginTransaction();

        Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters);

        Task<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>> where) 
            where TEntity : class;

        Task<bool> RegisterNew<TEntity>(TEntity entity)
            where TEntity : class;

        Task<bool> RegisterDirty<TEntity>(TEntity entity)
            where TEntity : class;

        Task<bool> RegisterClean<TEntity>(TEntity entity)
            where TEntity : class;

        Task<bool> RegisterDeleted<TEntity>(TEntity entity)
            where TEntity : class;

        Task<bool> CommitAsync();

        void Rollback();
    }
}
