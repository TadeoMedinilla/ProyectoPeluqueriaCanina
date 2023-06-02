using DataAccess.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Register
    {
        public EmployeeDTO? Reg_Employee { get; set; }
        public ClientDTO? Reg_Client { get; set; }
        public User Reg_User { get; set; }
    }
}
