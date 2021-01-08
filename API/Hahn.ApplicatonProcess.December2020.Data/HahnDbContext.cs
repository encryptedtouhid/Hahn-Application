using System;
using System.Collections.Generic;
using System.Text;
using Hahn.ApplicatonProcess.December2020.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Hahn.ApplicatonProcess.December2020.Data
{
    public class HahnDbContext : DbContext
    {
        public HahnDbContext(DbContextOptions<HahnDbContext> options)
            : base(options)
        {
        }

        public DbSet<Applicant> Applicants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Applicant>(
                b =>
                {
                    b.HasKey(x => x.ID);
                });
        }
    }
}
