using DataAccess.DAO;
using DataAccess.Entities;
using DataAccess.Entities.DTOs;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PeluqueriaCanina.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class RegisterController : ControllerBase
    {
        private Employee Emp { get; set; } = new Employee();
        private EmployeeDTO EmpDTO { get; set; } = new EmployeeDTO();
        private EmployeeDAO empDAO { get; set; } = new EmployeeDAO();

        private Client Cli { get; set; } = new Client();
        private ClientDTO CliDTO { get; set; } = new ClientDTO();
        
        //private ClientDAO CliDAO { get; set; } = new ClientDAO();
        private IClientDAO CliDAO { get; set; } 
        

        //private UserDAO userDAO { get; set; } = new UserDAO();
        private IUserDAO userDAO { get; set; }

        public RegisterController(IClientDAO cliDAO, IUserDAO userDAO)
        {
            CliDAO = cliDAO;
            this.userDAO = userDAO;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("/Client")]
        public async Task<ActionResult> RegisterClient([FromBody] Register cliRegistration)
        {
            //Search for another solution. 
            cliRegistration.Reg_Client.Cli_Role = 3;
            cliRegistration.Reg_Client.Cli_Status = 1;
            cliRegistration.Reg_User.User_Role = cliRegistration.Reg_Client.Cli_Role;

            try
            {
                //Insert to UserMaster table
                await userDAO.RegisterUser(cliRegistration);

                //Insert to Clients tables
                await CliDAO.RegisterClient(cliRegistration);

                return Ok("Registro realizado correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrio un error al registrarse.");
            }
        }

        [Authorize(policy: "Admin")]
        [HttpPost]
        [Route("/Employee")]
        public async Task<ActionResult> RegisterEmployee([FromBody] Register empRegistration)
        {
            try
            {
                // Insert to UserMaster table.
                await userDAO.RegisterUser(empRegistration);

                // Insert to Employees tables.
                await empDAO.RegisterEmployee(empRegistration);

                return Ok("Registro realizado correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrio un error al registrarse.");
            }
        }
    }
}
