using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Core;
using Task.Core.Domain;
using Task.Core.Repositories;
using Task.Implementation;
using Task.Implementation.Repository;

namespace Task
{
    public class UnitOfWork : IUnitOfWork
    {
        public IEmployeeRepository EmployeeRepo { get; set; }


        private readonly TaskContext _context;

        public UnitOfWork(TaskContext context)
        {
            _context = context;
            EmployeeRepo = new EmployeeRepository(context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch
            {
                _context.Dispose();
               return 0;
            }
        }
    }
}
