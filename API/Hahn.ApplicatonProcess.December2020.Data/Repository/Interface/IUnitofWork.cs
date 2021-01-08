using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Data.Repository.Interface
{
    public interface IUnitofWork
    {
        IDbContextTransaction BeginTransaction();
        Task<bool> SubmitChangesAsync();
    }
}
