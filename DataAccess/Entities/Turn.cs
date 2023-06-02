using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Turn
    {
        public int? Turn_ID { get; set; }
        public string? Turn_ClientEmail { get; set; }
        public int? Turn_ClientID { get; set; }
        public Employee? Turn_Employee { get; set; }
        public int? Turn_EmployeeID { get; set; }
        public string? Turn_EmployeeEmail { get; set; }
        public DateTime Turn_CheckIn { get; set; }
        public int? Turn_Status { get; set; }
        public bool? Availability { get; set; }
        public string? Turn_Code { get; set; }
    }
}
