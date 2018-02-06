using System.Collections.Generic;
using System.Threading.Tasks;
using Task.Core.Domain;
using Task.DTO.Request;

namespace Task.BusinessLayer.Interface
{
    public interface IEmployeeBusinessLayer
    {
        Task<IEnumerable<Employee>> GetAllEmployee();
        Task<Employee> GetEmployeeById(int id);
        Task<int> InsertEmployee(EmployeeRequest emp);
        Task<int> UpdateEmployee(int id, EmployeeRequest emp);
        Task<int> DeleteEmployee(int id);
    }
}
