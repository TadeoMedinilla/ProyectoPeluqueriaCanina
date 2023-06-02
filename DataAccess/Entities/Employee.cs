using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Employee
    {
        public int Emp_ID { get; set; }
        public string Emp_Name { get; set; }
        public string Emp_LastName { get; set; }
        public int Emp_DNI { get; set; }
        public string Emp_Adress { get; set; }
        public string Emp_Email { get; set; }
        public int Emp_Role { get; set; }
        public int Emp_Status { get; set; }
        public int Emp_CheckIn { get; set; }
    }

}
