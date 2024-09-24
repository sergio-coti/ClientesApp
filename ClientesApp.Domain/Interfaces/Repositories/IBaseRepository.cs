using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClientesApp.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Interface genérica para operações do repositório
    /// </summary>
    public interface IBaseRepository<TEntity, TKey> : IDisposable
        where TEntity : class
    {
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<List<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where);
        Task<TEntity?> GetOneAsync(Expression<Func<TEntity, bool>> where);
        Task<TEntity?> GetByIdAsync(TKey id);
    }
}
