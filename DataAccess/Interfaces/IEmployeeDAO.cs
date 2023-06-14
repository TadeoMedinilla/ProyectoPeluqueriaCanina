using DataAccess.Entities;
using DataAccess.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IEmployeeDAO
    {
        public Task<int> RegisterEmployee(Register empRegistration);
        public List<EmployeeDTO> GetEmployees();
        public Employee SelectEmployeeByID(Employee emp);
        public Employee SelectEmployeeByEmail(Employee emp);
    }
}
