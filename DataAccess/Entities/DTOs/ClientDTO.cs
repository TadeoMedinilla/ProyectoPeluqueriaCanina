using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities.DTOs
{
    public class ClientDTO
    {
        public int Cli_ID { get; set; }
        public string Cli_Name { get; set; }
        public string Cli_LastName { get; set; }
        public int Cli_DNI { get; set; }
        public string Cli_Adress { get; set; }
        public string Cli_Email { get; set; }
        public int Cli_Role { get; set; }
        public int Cli_Status { get; set; }
    }
}
