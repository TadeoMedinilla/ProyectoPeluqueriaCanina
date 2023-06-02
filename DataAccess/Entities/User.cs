using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class User
    {
        public int? User_ID { get; set; }
        public string User_Email { get; set; }
        public string User_Password { get; set; }
        public string? User_HashedPass { get; set; }
        public int? User_Role { get; set; }
    }
}
