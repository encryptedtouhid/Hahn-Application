using Hahn.ApplicatonProcess.December2020.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Data.Repository.Interface
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> ReadSingleAsync(int Id, bool WithTracking = true, string include = null);

        Task<IEnumerable<TEntity>> ReadByIdsAsync(int[] Id, bool WithTracking = false);

        Task<IEnumerable<TEntity>> ReadAllAsync();

        Task<TEntity> CreateAsync(TEntity TEntity);

        Task<bool> CreateMultipleAsync(IEnumerable<TEntity> TEntity);

        Task UpdateAsync(TEntity TEntity);

        Task UpdateMultipleAsync(IEnumerable<TEntity> TEntity);

        Task<bool> DeleteAsync(int Id);

        Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> SingleOrDefault(Expression<Func<TEntity, bool>> predicate, string[] ChildObjectNamesToInclude = null, bool WithTracking = false);

        Task<IEnumerable<TEntity>> GetWhere(Expression<Func<TEntity, bool>> predicate, string[] ChildObjectNamesToInclude = null, bool WithTracking = false);

        Task<int> CountAll();

        Task<int> CountWhere(Expression<Func<TEntity, bool>> predicate);
    }
}
