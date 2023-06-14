using DataAccess.DAO;
using DataAccess.Entities;
using DataAccess.Entities.DTOs;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PeluqueriaCanina.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class ClientController : ControllerBase
    {
        private Client Cli { get; set; } = new Client();
        private ClientDTO CliDTO { get; set; } = new ClientDTO();
        private IClientDAO CliDAO { get; set; } 
        private UserDAO userDAO { get; set; } = new UserDAO();

        private Register Registration { get; set; } = new Register();


        public ClientController(IClientDAO cliDAO)
        {
            CliDAO = cliDAO;
        }

        //Por cuestiones de orden este metodo se paso a RegisterController.

        //[HttpPost]
        //[Route("RegisterClient")]
        //public async Task RegisterClient([FromBody] Register cliRegistration)
        //{
        //    //Esto se deberia hacer en el front
        //    cliRegistration.Reg_Client.Cli_Role = 3;
        //    cliRegistration.Reg_Client.Cli_Status = 1;
        //    cliRegistration.Reg_User.User_Role = cliRegistration.Reg_Client.Cli_Role;

        //    //Insert to UserMaster table
        //    await userDAO.RegisterUser(cliRegistration);

        //    //Insert to Clients tables
        //    await CliDAO.RegisterClient(cliRegistration);
        //}
    }
}
