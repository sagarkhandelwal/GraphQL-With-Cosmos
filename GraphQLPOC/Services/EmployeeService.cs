using GraphQLPOC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLPOC.Services
{
    public class EmployeeService : IEmployeeService
    {
        #region Property  
        private readonly DatabaseContext _dbContext;
        #endregion

        #region Constructor  
        public EmployeeService(DatabaseContext databaseContext)
        {
            _dbContext = databaseContext;
        }
        #endregion

        public async Task<Employee> Create(Employee employee)
        {

            var data = new Employee
            {
                Name = employee.Name,
                Designation = employee.Designation,
                PreviousCompanies = new List<Company>(employee.PreviousCompanies)
                
            };
            await _dbContext.Employees.AddAsync(data);
            await _dbContext.SaveChangesAsync();
            return data;
        }
        public async Task<bool> Delete(DeleteVM deleteVM)
        {
            var employee = await _dbContext.Employees.FirstOrDefaultAsync(c => c.Name == deleteVM.Name);
            if (employee is not null)
            {
                _dbContext.Employees.Remove(employee);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public IQueryable<Employee> GetAll()
        {
            return _dbContext.Employees.AsQueryable();
        }

        public async Task<Employee> Update(Employee employee)
        {
            var entity = await _dbContext.Employees.FirstOrDefaultAsync(item => item.Id == employee.Id);

            if (entity != null)
            {
                if (employee.Name != null)
                {
                    entity.Name = employee.Name;
                }
                if (employee.Designation != null)
                {
                    entity.Designation = employee.Designation;
                }
                if (employee.PreviousCompanies != null)
                {
                    entity.PreviousCompanies = new List<Company>(employee.PreviousCompanies);
                }

                await _dbContext.SaveChangesAsync();
            }
            return entity;

        }
    }

    public class DeleteVM
    {
        public string Name { get; set; }
    }
}
