using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Awaitable = System.Threading.Tasks;
using Task.BusinessLayer.Interface;
using Task.Core;
using Task.Core.Domain;
using Task.Core.Repositories;
using Task.DTO.Request;
using Task.Implementation;

namespace Task.BusinessLayer.Implementation
{
    public class EmployeeBusinessLayer : IEmployeeBusinessLayer
    {
        private TaskContext _context { get; set; }

        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeBusinessLayer(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Awaitable.Task<IEnumerable<Employee>> GetAllEmployee()
        {
            return await _employeeRepository.GetAllAsync();
        }

        public async Awaitable.Task<Employee> GetEmployeeById(int id)
        {
            return await _employeeRepository.GetAsync(id);
        }

        public async Awaitable.Task InsertEmployee(EmployeeRequest emp)
        {
            var employee = Mapper.Map<Employee>(emp);
            await _employeeRepository.Add(employee);
        }

        public async Awaitable.Task UpdateEmployee(int id, EmployeeRequest emp)
        {
            var existingInfo = await _employeeRepository.GetAsync(id);
            var newInfo = Mapper.Map<Employee>(emp);

            var employee = Mapper.Map(newInfo, existingInfo);
            employee.Id = existingInfo.Id;

            await _employeeRepository.SaveOrUpdate(employee);
        }

        public async Awaitable.Task DeleteEmployee(int id)
        {
            var existingInfo = await _employeeRepository.GetAsync(id);
            var employee = Mapper.Map<Employee>(existingInfo);
            await _employeeRepository .DeleteOnSubmit(employee);
        }
    }
}
