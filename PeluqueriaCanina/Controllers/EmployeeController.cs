using DataAccess.DAO;
using DataAccess.Entities;
using DataAccess.Entities.DTOs;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PeluqueriaCanina.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class EmployeeController : ControllerBase
    {
        private Employee Emp { get; set; } = new Employee();
        private EmployeeDTO EmpDTO { get; set; } = new EmployeeDTO();
        private List<EmployeeDTO> EmpDTOList { get; set; } = new List<EmployeeDTO> ();
        private List<Employee> EmpList { get; set; } = new List<Employee> ();
        //private EmployeeDAO empDAO { get; set; } = new EmployeeDAO();
        private IEmployeeDAO empDAO { get; set; }

        private UserDAO userDAO { get; set; } = new UserDAO();

        private Register Registration { get; set; } = new Register();
        

        public EmployeeController(IEmployeeDAO empDAO)
        {
            this.empDAO = empDAO;
        }

        /* Por cuestiones de orden este metodo se paso a RegisterController.
        [HttpPost]
        [Route("RegisterEmployee")]
        public async Task RegisterEmployee([FromBody] Register empRegistration)
        {
            //Insert to UserMaster table.

            await userDAO.RegisterUser(empRegistration);

            //Insert to Employees tables.

            await empDAO.RegisterEmployee(empRegistration);

        }*/

        [AllowAnonymous]
        //[Authorize(policy:"Client")]
        [HttpGet]
        [Route("/Employees")]
        public ActionResult<List<EmployeeDTO>> Employees()
        {
            EmpDTOList = empDAO.GetEmployees();

            return Ok(EmpDTOList);
        }

        //[HttpGet]
        //public ActionResult SeleccionarEmpleado(int emp)
        //{
        //    Emp.Emp_ID = emp;
        //    Emp.Emp_Status = 1;
        //    Emp = empDAO.SelectEmployeeByID(Emp);
        //    return Ok(Emp);
        //}
    }
}
