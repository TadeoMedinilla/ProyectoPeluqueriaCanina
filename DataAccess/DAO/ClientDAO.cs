using DataAccess.Configuration;
using DataAccess.Entities;
using DataAccess.Entities.DTOs;
using DataAccess.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class ClientDAO : SQL_Methods<Client>
    {
        private Automapper Mapper { get; set; } = new Automapper();

        private Client cli { get; set; } = new Client();
        private ClientDTO cliDTO { get; set; } = new ClientDTO();
        private List<Client> clients { get; set; } = new List<Client>();
        private List<ClientDTO> clientsDTO { get; set; } = new List<ClientDTO>();


        //Database Access:

        //Insert Client querys:

        private string ClientMaster_Insert { get; } = "INSERT INTO PeluqueriaCanina.dbo.ClientMaster ( CliM_Name, CliM_LastName)" +
                                                        "\r\n  VALUES (@Cli_Name,@Cli_LastName)";
        private string ClientDetail_Insert { get; } ="     SET IDENTITY_INSERT PeluqueriaCanina.dbo.ClientDetail ON;" +
                                                    "\r\n  INSERT INTO PeluqueriaCanina.dbo.ClientDetail (CliD_ID, CliD_DNI, CliD_Adress, CliD_Email, CliD_Role, CliD_Status)" +
                                                    "\r\n  SELECT CliM_ID, @Cli_DNI, @Cli_Adress, @Cli_Email, @Cli_Role, @Cli_Status" +
                                                    "\r\n  FROM PeluqueriaCanina.dbo.ClientMaster" +
                                                    "\r\n  WHERE CliM_Name = @Cli_Name AND CliM_LastName = @Cli_LastName;" +
                                                    "\r\n  SET IDENTITY_INSERT PeluqueriaCanina.dbo.EmployeeDetail OFF;";

        //Select Client querys:

        private string ClientMaster_Select { get; } = string.Empty;
        private string ClientDetail_Select { get; } = string.Empty;

        public async Task<int> RegisterClient(Register cliRegistration)
        {
            cli = Mapper.mapper.Map<Client>(cliRegistration.Reg_Client);

            int affectedRows_1 = await SQL_Executable(ClientMaster_Insert, cli);

            int affectedRows_2 = await SQL_Executable(ClientDetail_Insert, cli);

            if (affectedRows_1 !=0 &&  affectedRows_2 != 0) { return 1; }
            else { return 0; }
        }

    }
}
