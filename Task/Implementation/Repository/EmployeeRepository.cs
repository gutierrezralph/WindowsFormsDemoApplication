using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Core.Domain;
using Task.Core.Repositories;

namespace Task.Implementation.Repository
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(TaskContext context) : base (context)
        {

        }
    }
}
