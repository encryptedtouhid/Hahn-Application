using Hahn.ApplicatonProcess.December2020.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Domain.BusinessLogics.Interface
{
    public interface IBaseService<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> ReadSingleAsync(int Id, bool WithTracking = true, string include = null);

        Task<IEnumerable<TEntity>> ReadAllAsync();

        Task<TEntity> CreateAsync(TEntity TEntity);

        Task<bool> CreateMultipleAsync(IEnumerable<TEntity> TEntity);

        Task UpdateAsync(TEntity TEntity);

        Task UpdateMultipleAsync(IEnumerable<TEntity> TEntity);

        Task<bool> DeleteAsync(int Id);

    }
}
