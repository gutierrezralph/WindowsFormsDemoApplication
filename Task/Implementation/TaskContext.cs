using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Core.Domain;
using Task.Persistence.EntityConfigurations;

namespace Task.Implementation
{
    public class TaskContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public new DbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EmployeeConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
