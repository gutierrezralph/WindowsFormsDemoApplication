using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Awaitable = System.Threading.Tasks;
using Task.BusinessLayer.Interface;
using Task.Core;
using Task.Core.Domain;
using Task.DTO.Request;
using Task.Implementation;

namespace Task.BusinessLayer.Implementation
{
    public class EmployeeBusinessLayer : IEmployeeBusinessLayer
    {
        private TaskContext _context { get; set; }

        private IUnitOfWork _unitOfWork = new UnitOfWork(new TaskContext());

        public async Task<IEnumerable<Employee>> GetAllEmployee()
        {
            try
            {
                return await _unitOfWork.EmployeeRepo.GetAllAsync();
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            return await _unitOfWork.EmployeeRepo.GetAsync(id);
        }

        public async Task<int> InsertEmployee(EmployeeRequest emp)
        {
            var employee = Mapper.Map<Employee>(emp);
            _unitOfWork.EmployeeRepo.Add(employee);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<int> UpdateEmployee(int id, EmployeeRequest emp)
        {
            var existingInfo = await _unitOfWork.EmployeeRepo.GetAsync(id);
            var newInfo = Mapper.Map<Employee>(emp);

            var employee = Mapper.Map(newInfo, existingInfo);
            employee.Id = existingInfo.Id;

            _unitOfWork.EmployeeRepo.SaveOrUpdate(employee);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<int> DeleteEmployee(int id)
        {
            var existingInfo = await _unitOfWork.EmployeeRepo.GetAsync(id);
            var employee = Mapper.Map<Employee>(existingInfo);
            _unitOfWork.EmployeeRepo.Remove(employee);
            return await _unitOfWork.SaveChangesAsync();
        }
    }
}
