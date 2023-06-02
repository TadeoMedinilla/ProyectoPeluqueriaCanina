using DataAccess.Configuration;
using DataAccess.Entities;
using DataAccess.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class UserDAO : SQL_Methods<User>
    {
        private PasswordHandler passwordHandler = new PasswordHandler(); 
        private Automapper Mapper { get; set; } = new Automapper();

        private User user { get; set; } = new User();

        // Insert User querys:
        private string UserMaster_Insert { get; } = "INSERT INTO PeluqueriaCanina.dbo.UserMaster (UserM_Email, UserM_HashedPass, UserM_Role)\r\nVALUES (@User_Email,@User_HashedPass, @User_Role);\r\n";

        // Select Employee querys:
        private string GetUser_String { get; set; } = "SELECT UserM_Email as User_Email, UserM_HashedPass as User_HashedPass, UserM_Role as User_Role\r\nFROM PeluqueriaCanina.dbo.UserMaster\r\nWHERE UserM_Email = @User_Email";

        public async Task<int> RegisterUser(Register userRegistration)
        {
            user = userRegistration.Reg_User;
            user.User_HashedPass = passwordHandler.HashPass(user.User_Password);
            
            int affectedRows = await SQL_Executable(UserMaster_Insert, user);

            return affectedRows;
        }

        public User GetUser(User toSearch)
        {
            user = SQL_QueryFirstOrDefault(GetUser_String, toSearch);
           
            return user;
        }
    }
}
