using System.Collections.Generic;
using Awaitable = System.Threading.Tasks;
using Task.Core.Domain;
using Task.DTO.Request;

namespace Task.BusinessLayer.Interface
{
    public interface IEmployeeBusinessLayer
    {
        Awaitable.Task<IEnumerable<Employee>> GetAllEmployee();
        Awaitable.Task<Employee> GetEmployeeById(int id);
        Awaitable.Task InsertEmployee(EmployeeRequest emp);
        Awaitable.Task UpdateEmployee(int id, EmployeeRequest emp);
        Awaitable.Task DeleteEmployee(int id);
    }
}
