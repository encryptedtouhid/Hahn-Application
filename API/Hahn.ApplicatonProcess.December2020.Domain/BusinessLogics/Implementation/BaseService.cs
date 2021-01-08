using Hahn.ApplicatonProcess.December2020.Data.Models;
using Hahn.ApplicatonProcess.December2020.Data.Repository.Interface;
using Hahn.ApplicatonProcess.December2020.Domain.BusinessLogics.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Domain.BusinessLogics.Implementation
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        protected readonly IBaseRepository<TEntity> _baseRepository;
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<TEntity> _logger;

        public BaseService(IBaseRepository<TEntity> baseRepository, IUnitofWork unitofWork, ILogger<TEntity> logger)
        {
            this._baseRepository = baseRepository;
            this._unitofWork = unitofWork;
            _logger = logger;
        }

        public async Task<TEntity> CreateAsync(TEntity TEntity)
        {
            try
            {
                var entity = await _baseRepository.CreateAsync(TEntity);
                await _unitofWork.SubmitChangesAsync();
                return entity;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<bool> CreateMultipleAsync(IEnumerable<TEntity> TEntity)
        {
            try
            {
                await _baseRepository.CreateMultipleAsync(TEntity);
                await _unitofWork.SubmitChangesAsync();
                return true;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            try
            {
                await _baseRepository.DeleteAsync(Id);
                await _unitofWork.SubmitChangesAsync();
                return true;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            };
        }

        public async Task<IEnumerable<TEntity>> ReadAllAsync()
        {
            return await _baseRepository.ReadAllAsync();
        }

        public async Task<TEntity> ReadSingleAsync(int Id, bool WithTracking = true, string include = null)
        {
            return await _baseRepository.ReadSingleAsync(Id, WithTracking, include);
        }

        public async Task UpdateAsync(TEntity TEntity)
        {
            try
            {
                await _baseRepository.UpdateAsync(TEntity);
                await _unitofWork.SubmitChangesAsync();
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        public async Task UpdateMultipleAsync(IEnumerable<TEntity> TEntity)
        {
            try
            {
                await _baseRepository.UpdateMultipleAsync(TEntity);
                await _unitofWork.SubmitChangesAsync();
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}
