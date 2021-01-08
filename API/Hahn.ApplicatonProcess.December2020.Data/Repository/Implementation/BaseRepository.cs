using Hahn.ApplicatonProcess.December2020.Data.Models;
using Hahn.ApplicatonProcess.December2020.Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Data.Repository.Implementation
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly HahnDbContext _context;

        public BaseRepository(HahnDbContext dbContext)
        {
            this._context = dbContext;
        }

        public async Task<int> CountAll() => await _context.Set<TEntity>().CountAsync();

        public async Task<int> CountWhere(Expression<Func<TEntity, bool>> predicate) => await _context.Set<TEntity>().CountAsync(predicate);

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            var returnItem = await _context.Set<TEntity>().AddAsync(entity);
            return returnItem.Entity;
        }

        public async Task<bool> CreateMultipleAsync(IEnumerable<TEntity> entity)
        {
            try
            {
                await _context.Set<TEntity>().AddRangeAsync(entity);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            try
            {
                TEntity entity = await ReadSingleAsync(Id);
                _context.Set<TEntity>().Remove(entity);
                return true;
            }
            catch (Exception ex)
            {
                //log error info to entral logging system.
                return false;
            }
        }

        public async Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> predicate) => await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);

        public async Task<TEntity> SingleOrDefault(Expression<Func<TEntity, bool>> predicate, string[] ChildObjectNamesToInclude = null, bool WithTracking = false)
        {
            TEntity data;
            if (ChildObjectNamesToInclude != null && ChildObjectNamesToInclude.Count() > 0)
            {
                var queryable = _context.Set<TEntity>().AsQueryable();
                foreach (var item in ChildObjectNamesToInclude)
                {
                    queryable = queryable.Include(item);
                }
                if (WithTracking)
                    data = await queryable.SingleOrDefaultAsync(predicate);
                else
                    data = await queryable.AsNoTracking().SingleOrDefaultAsync(predicate);
            }
            else
            {
                if (WithTracking)
                    data = await _context.Set<TEntity>().SingleOrDefaultAsync(predicate);
                else
                    data = await _context.Set<TEntity>().AsNoTracking().SingleOrDefaultAsync(predicate);
            }
            return data;
        }

        public async Task<IEnumerable<TEntity>> GetWhere(Expression<Func<TEntity, bool>> predicate, string[] ChildObjectNamesToInclude = null, bool WithTracking = false)
        {
            List<TEntity> data = new List<TEntity>();
            if (ChildObjectNamesToInclude != null && ChildObjectNamesToInclude.Count() > 0)
            {
                var queryable = _context.Set<TEntity>().AsQueryable();
                foreach (var item in ChildObjectNamesToInclude)
                {
                    queryable = queryable.Include(item);
                }
                if (WithTracking)
                    data = await queryable.Where(predicate).ToListAsync();
                else
                    data = await queryable.Where(predicate).AsNoTracking().ToListAsync();
            }
            else
            {
                if (WithTracking)
                    data = await _context.Set<TEntity>().Where(predicate).ToListAsync();
                else
                    data = await _context.Set<TEntity>().Where(predicate).AsNoTracking().ToListAsync();
            }
            return data;
        }

        public async Task<IEnumerable<TEntity>> ReadAllAsync()
        {
            var returnItem = await _context.Set<TEntity>().AsNoTracking().ToListAsync<TEntity>();
            return returnItem;
        }

        public async Task<IEnumerable<TEntity>> ReadByIdsAsync(int[] Id, bool WithTracking = false)
        {
            IEnumerable<TEntity> returnItem;
            if (WithTracking)
                returnItem = await _context.Set<TEntity>().Where(a => Id.Contains(a.ID)).ToListAsync<TEntity>();
            returnItem = await _context.Set<TEntity>().AsNoTracking().Where(a => Id.Contains(a.ID)).ToListAsync<TEntity>();
            return returnItem;
        }

        public async Task<TEntity> ReadSingleAsync(int Id, bool WithTracking = true, string include = null)
        {
            TEntity returnItem;
            if (WithTracking)
                if (!string.IsNullOrEmpty(include))
                    returnItem = await _context.Set<TEntity>().Include(include).SingleOrDefaultAsync(x => x.ID == Id);
                else
                    returnItem = await _context.Set<TEntity>().SingleOrDefaultAsync(x => x.ID == Id);
            else
            {
                if (!string.IsNullOrEmpty(include))
                    returnItem = await _context.Set<TEntity>().Include(include).AsNoTracking().SingleOrDefaultAsync(x => x.ID == Id);
                else
                    returnItem = await _context.Set<TEntity>().AsNoTracking().SingleOrDefaultAsync(x => x.ID == Id);
            }
            return returnItem;
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await Task.Run(() => _context.Entry(entity).State = EntityState.Modified);
        }

        public async Task UpdateMultipleAsync(IEnumerable<TEntity> entity)
        {
            foreach (var item in entity)
            {
                await Task.Run(() => _context.Entry(item).State = EntityState.Modified);
            }
        }
    }
}
