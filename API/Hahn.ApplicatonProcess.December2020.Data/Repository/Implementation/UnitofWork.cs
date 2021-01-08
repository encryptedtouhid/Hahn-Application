using Hahn.ApplicatonProcess.December2020.Data.Repository.Interface;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Data.Repository.Implementation
{
    public class UnitofWork : IUnitofWork
    {
        private readonly HahnDbContext _context;

        public UnitofWork(HahnDbContext dataEngineDbContext)
        {
            this._context = dataEngineDbContext;
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }

        public async Task<bool> SubmitChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
