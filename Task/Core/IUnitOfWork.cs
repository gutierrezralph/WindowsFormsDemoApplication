using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Core.Domain;
using Task.Core.Repositories;

namespace Task.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository EmployeeRepo { get; }
        Task<int> SaveChangesAsync();
    }
}
