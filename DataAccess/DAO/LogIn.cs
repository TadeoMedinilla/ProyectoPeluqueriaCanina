using DataAccess.Entities;
using DataAccess.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    [ApiController]
    [Route("[Controller]")]
    public class LogIn : ControllerBase
    {
        private PasswordHandler passwordHandler { get; set; } = new PasswordHandler();
        private TokenManagement TokenManager { get; set; } = new TokenManagement();
        private UserDAO userDAO { get; set; } = new UserDAO();
        private User user { get; set; } = new User();


        [HttpPost]
        public IActionResult Log_In([FromBody] User userCredentials)
        {
            user = userDAO.GetUser(userCredentials);
            user.User_Password = userCredentials.User_Password;
            if (IsValidUser(user))
            {
                var token = TokenManager.GenerateJWTToken(user);
                Response.Headers.Add("Authorization", "Bearer " + token);
                return Ok($"Bienvenido");
            }

            else{
                return BadRequest("Las credenciales de usuario son incorrectas.");
            }
        }

        private bool IsValidUser(User toVerify)
        {
            bool passValidation = passwordHandler.VerifyPass(toVerify.User_Password, toVerify.User_HashedPass);
         
            return passValidation;
        }

    }
}
