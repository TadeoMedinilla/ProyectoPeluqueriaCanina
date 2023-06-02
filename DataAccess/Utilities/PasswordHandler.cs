using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Utilities
{
    public class PasswordHandler
    {
        public string HashPass(string password)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt();

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);

            return hashedPassword;
        }

        public bool VerifyPass(string password, string hashedPassword)
        {
            bool Verification = BCrypt.Net.BCrypt.Verify(password, hashedPassword);

            return Verification;
        }
    }
}
