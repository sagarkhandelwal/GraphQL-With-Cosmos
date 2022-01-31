using GraphQLPOC.Models;
using GraphQLPOC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLPOC.GraphQL
{
    public class Query
    {
        #region Property  
        private readonly IEmployeeService _employeeService;
        #endregion

        #region Constructor  
        public Query(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        #endregion

        public IQueryable<Employee> Employees => _employeeService.GetAll();
    }
}
