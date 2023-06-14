using DataAccess.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface ITurnDAO
    {
        public List<TurnDTO> ReservedTurnList(string empEmail, TurnDTO turnDTO);

        public List<TurnDTO> ReservedTurnList_ForClients(DateTime turnDate, string userEmail);

        public List<TurnDTO> EmployeeAvailableTurns(int empID, DateTime turnDate);

        public Task<string> InsertTurn(TurnDTO turnDTO);
    }
}
