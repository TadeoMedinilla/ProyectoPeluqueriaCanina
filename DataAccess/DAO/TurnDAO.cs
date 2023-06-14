using DataAccess.Configuration;
using DataAccess.Entities;
using DataAccess.Entities.DTOs;
using DataAccess.Interfaces;
using DataAccess.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class TurnDAO : SQL_Methods<Turn>, ITurnDAO
    {
        private int durationInMinutes { get; set; } = 30;
        private Turn turn { get; set; } = new Turn();
        private List<Turn> turnList { get; set; } = new List<Turn>();
        private TurnDTO turnDTO { get; set; } = new TurnDTO();
        private List<TurnDTO> turnDTOList { get; set; } = new List<TurnDTO>();
        private Employee emp { get; set; } = new Employee();
        private EmployeeDTO empDTO { get; set; } = new EmployeeDTO();
        private EmployeeDAO empDAO { get; set; } = new EmployeeDAO();

        private Automapper Mapper { get; set; } = new Automapper();

        //DataBase Access 

        //Select querys:
        private string TurnMaster_SelectAll { get; set; } = "SELECT TurnM_CliID as Turn_CliID, TurnDetail.TurnD_CheckIn as Turn_CheckIn, TurnDetail.TurnD_Status as Turn_Status" +
                                                            "\r\nFROM PeluqueriaCanina.dbo.TurnMaster\r\nFULL JOIN PeluqueriaCanina.dbo.TurnDetail on TurnD_ID = TurnM_ID" +
                                                            "\r\nWHERE TurnM_EmpID = @Turn_EmployeeID;";
        private string TurnMaster_Select { get; set; } = "SELECT TurnM_CliID as Turn_CliID, TurnDetail.TurnD_CheckIn as Turn_CheckIn, TurnDetail.TurnD_Status as Turn_Status" +
                                                        "\r\nFROM PeluqueriaCanina.dbo.TurnMaster\r\nFULL JOIN PeluqueriaCanina.dbo.TurnDetail on TurnD_ID = TurnM_ID" +
                                                        "\r\nWHERE  TurnD_CheckIn LIKE '%@Turn_CheckIn%' AND TurnM_EmpID = @Turn_EmployeeID AND TurnD_Status = @Turn_Status;";
        
        private string TurnDetail_Select { get; set; } = "SELECT TurnD_ID as Turn_ID, TurnD_Code as Turn_Code, TurnD_CheckIn as Turn_CheckIn, TurnM_CliID as Turn_ClientID, TurnM_EmpID as Turn_EmployeeID " +
                                                            "FROM PeluqueriaCanina.dbo.TurnDetail " +
                                                            "FULL JOIN PeluqueriaCanina.dbo.TurnMaster ON TurnD_ID = TurnM_ID " +
                                                            "FULL JOIN PeluqueriaCanina.dbo.EmployeeDetail ON TurnM_EmpID = EmpD_ID " +
                                                            "WHERE EmpD_Email = @Turn_EmployeeEmail " +
                                                            "AND YEAR(TurnD_CheckIn) = YEAR(@Turn_CheckIn) " +
                                                            "AND MONTH(TurnD_CheckIn) = MONTH(@Turn_CheckIn)" +
                                                            "AND DAY(TurnD_CheckIn) = DAY(@Turn_CheckIn)";
        private string TurnDetail_Select_ForClients { get; set; } = "SELECT TurnD_ID as Turn_ID, TurnD_Code as Turn_Code, TurnD_CheckIn as Turn_CheckIn, TurnM_CliID as Turn_ClientID, TurnM_EmpID as Turn_EmployeeID " +
                                                            "FROM PeluqueriaCanina.dbo.TurnDetail " +
                                                            "FULL JOIN PeluqueriaCanina.dbo.TurnMaster ON TurnD_ID = TurnM_ID " +
                                                            "FULL JOIN PeluqueriaCanina.dbo.ClientDetail ON TurnM_CliID = CliD_ID " +
                                                            "WHERE CliD_Email = @Turn_ClientEmail " +
                                                            "AND YEAR(TurnD_CheckIn) = YEAR(@Turn_CheckIn) " +
                                                            "AND MONTH(TurnD_CheckIn) = MONTH(@Turn_CheckIn)" +
                                                            "AND DAY(TurnD_CheckIn) = DAY(@Turn_CheckIn)";


        //Insert Querys:
        private string TurnMaster_Insert { get; set; } = "INSERT INTO PeluqueriaCanina.dbo.TurnMaster (TurnM_CliID, TurnM_EmpID, TurnM_Code)" +
                                                            "\r\nSELECT  CliD_ID, @Turn_EmployeeID, @Turn_Code" +
                                                            "    FROM PeluqueriaCanina.dbo.ClientDetail" +
                                                            "    WHERE CliD_Email = @Turn_ClientEmail;";
        private string TurnDetail_Insert { get; set; } = "INSERT INTO PeluqueriaCanina.dbo.TurnDetail (TurnD_ID, TurnD_Code, TurnD_CheckIn, TurnD_Status) " +
                                                        "\r\nSELECT TurnM_ID,  @Turn_Code, @Turn_CheckIn, @Turn_Status\r\nFROM PeluqueriaCanina.dbo.TurnMaster" +
                                                        "\r\nWHERE TurnM_Code = @Turn_Code";

        private List<TurnDTO> TurnGenerator(Employee emp, DateTime turnDate)
        {
            DateTime empStartTime = turnDate.AddHours(emp.Emp_CheckIn);
            DateTime empFinishTime = empStartTime.AddHours(8);

            TimeSpan turnDuration = TimeSpan.FromMinutes(durationInMinutes);

            DateTime currentTime = empStartTime;

            int counter = 1;

            while ( currentTime.Add(turnDuration) <= empFinishTime )
            {
                TurnDTO auxTurn = new TurnDTO();

                
                auxTurn.Turn_ID = counter;
                auxTurn.Turn_EmployeeID = emp.Emp_ID;
                auxTurn.Turn_CheckIn = currentTime;
                auxTurn.Availability = true;

                turnDTOList.Add(auxTurn);

                currentTime = currentTime.Add(turnDuration);
                counter++;
            }

            return turnDTOList;
        }

        /// <summary>
        /// Its for Employees, returns the booked turns of an employee
        /// regardless of wich customer reserved it. 
        /// </summary>
        /// <param name="empDTO"></param>
        /// <param name="turnDTO"></param>
        /// <returns></returns>
        public List<TurnDTO> ReservedTurnList(string empEmail, TurnDTO turnDTO)
        {
            emp.Emp_Email = empEmail;
            emp.Emp_Status = 1;
            emp = empDAO.SelectEmployeeByEmail(emp);

            turnDTO.Turn_EmployeeID = emp.Emp_ID;
            turnDTO.Turn_EmployeeEmail = empEmail;
            turnDTO.Turn_Code = TurnCodeGenerator(turn);

            turn = Mapper.mapper.Map<Turn>(turnDTO);

            turnList = SQL_Query(TurnDetail_Select, turn);

            //Es necesario mapear a DTO la lista aca?
            foreach (Turn turn in turnList) 
            {
                turnDTO = Mapper.mapper.Map<TurnDTO>(turn);
                turnDTOList.Add(turnDTO);
            }

            return turnDTOList;
        }

        public List<TurnDTO> ReservedTurnList_ForClients( DateTime turnDate, string userEmail)
        {
            //turnDTO.Turn_Code = TurnCodeGenerator(turn);

            turn.Turn_CheckIn = turnDate;
            turn.Turn_ClientEmail = userEmail;

            turnList = SQL_Query(TurnDetail_Select_ForClients, turn);

            //Es necesario mapear a DTO la lista aca?

            foreach (Turn turn in turnList)
            {
                turnDTO = Mapper.mapper.Map<TurnDTO>(turn);
                turnDTOList.Add(turnDTO);
            }

            return turnDTOList;
        }



        /// <summary>
        /// For Clients.
        /// </summary>
        /// <param name="empID"></param>
        /// <param name="turnDate"></param>
        /// <returns></returns>
        public List<TurnDTO> EmployeeAvailableTurns(int empID, DateTime turnDate)
        {
            emp.Emp_ID = empID;
            emp.Emp_Status = 1;
            emp = empDAO.SelectEmployeeByID(emp);

            if (emp == null)
            {
                return null;
            }

            turnDate = turnDate.Date;

            List<TurnDTO> AvailableTurns = new List<TurnDTO>();
            AvailableTurns = TurnGenerator(emp, turnDate);

            turnDTO.Turn_EmployeeID = emp.Emp_ID;
            turnDTO.Turn_Status = 1;
            turnDTO.Turn_CheckIn = turnDate;

            List<TurnDTO> ReservedTurns = new List<TurnDTO>();
            ReservedTurns = ReservedTurnList(emp.Emp_Email, turnDTO);

            foreach (var resTurn in ReservedTurns)
            {
                foreach (var avTurn in AvailableTurns)
                {
                    if (DateTime.Compare(avTurn.Turn_CheckIn.Date, resTurn.Turn_CheckIn.Date) == 0
                        && avTurn.Turn_CheckIn.Hour == resTurn.Turn_CheckIn.Hour
                        && avTurn.Turn_CheckIn.Minute == resTurn.Turn_CheckIn.Minute)
                    {
                        avTurn.Availability = false;
                    }
                }
            }

            return AvailableTurns;
        }

        public async Task<string> InsertTurn(TurnDTO turnDTO)
        {
            int status = 1;

            turnDTO.Turn_Status = status;

            turn = Mapper.mapper.Map<Turn>(turnDTO);
            string code = TurnCodeGenerator(turn);
            turn.Turn_Code = code;

            await SQL_Executable(TurnMaster_Insert, turn);
            
            await SQL_Executable(TurnDetail_Insert, turn);

            return code;

        }

        private string TurnCodeGenerator(Turn turn)
        {
            string code = $"Emp0{turn.Turn_EmployeeID}-{turn.Turn_CheckIn.ToShortDateString()}";
            return code;
        }
    }
}

//{
//    "turn_ClientID": 1,
//  "turn_EmployeeID": 7,
//  "turn_CheckIn": "2023-05-22T10:30:00.000Z"
//}
