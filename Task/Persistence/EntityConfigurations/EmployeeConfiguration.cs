using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Core.Domain;

namespace Task.Persistence.EntityConfigurations
{
    public class EmployeeConfiguration : EntityTypeConfiguration<Employee>
    {
        public EmployeeConfiguration()
        {
            Property(p => p.Id)
                .IsRequired();

            Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(50);

            Property(p => p.DateCreated)
                .IsRequired();
               
        }
    }
}
