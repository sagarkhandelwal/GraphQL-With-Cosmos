using GraphQLPOC.Models;
using GraphQLPOC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLPOC.GraphQL
{
    public class Mutation
    {
        #region Property  
        private readonly IEmployeeService _employeeService;
        #endregion

        #region Constructor  
        public Mutation(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        #endregion
        public async Task<Employee> Create(Employee employee) => await _employeeService.Create(employee);
        public async Task<bool> Delete(DeleteVM deleteVM) => await _employeeService.Delete(deleteVM);
        public async Task<Employee> Update(Employee employee) => await _employeeService.Update(employee);
    }
}
