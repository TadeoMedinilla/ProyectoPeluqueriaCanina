using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities.DTOs
{
    public class TurnDTO
    {
        public int? Turn_ID { get; set; }
        public int Turn_ClientID { get; set; }
        public string Turn_ClientEmail { get; set; }
        public string Turn_EmployeeEmail { get; set; }
        public int Turn_EmployeeID { get; set; }
        public DateTime Turn_CheckIn { get; set; }
        public int? Turn_Status { get; set; }
        public bool? Availability { get; set; }
        public string? Turn_Code { get; set; }
    }
}
