using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLPOC.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }

  
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasNoDiscriminator()
                .ToContainer("Container1")
                .HasPartitionKey(da => da.Id)
                .HasKey(da => new { da.Id });

            modelBuilder.Entity<Employee>().OwnsMany(f => f.PreviousCompanies);
        }
    }
}
