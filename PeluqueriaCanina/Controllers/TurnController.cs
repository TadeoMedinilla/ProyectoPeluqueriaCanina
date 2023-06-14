using DataAccess.DAO;
using DataAccess.Entities;
using DataAccess.Entities.DTOs;
using DataAccess.Interfaces;
using DataAccess.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PeluqueriaCanina.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class TurnController : ControllerBase
    {
        //private TurnDAO turnDAO { get; set; } = new TurnDAO();
        private ITurnDAO turnDAO { get; set; } 

        private EmployeeDTO empDTO { get; set; } = new EmployeeDTO();

        private TurnDTO turnDTO { get; set; } = new TurnDTO();  
        private List<Turn> turnList { get; set; } = new List<Turn>();
        private List<TurnDTO> turnDTOList { get; set; } = new List<TurnDTO>();

        private User user { get; set; } = new User();

        private TokenManagement tokenManager { get; set; } = new TokenManagement();



        public TurnController(ITurnDAO turnDAO)
        {
            this.turnDAO = turnDAO;
        }

        [Authorize(policy:"Client")]
        [HttpGet]
        [Route("/AvailableTurns/{turnDate}/{empID}")]
        public ActionResult AvailableTurns(DateTime turnDate, int empID)
        {
            turnDTOList = turnDAO.EmployeeAvailableTurns(empID, turnDate);

            if (turnDTOList != null) { return Ok(turnDTOList); }
            
            else { return BadRequest("El empleado no existe"); }

        }

        [Authorize(policy:"Client")]
        [HttpPost]
        [Route("/BookTurn")]
        public async Task<ActionResult> BookTurnAsync([FromBody] DateTime turnDate, int empID)
        {
            turnDTO.Turn_EmployeeID = empID;
            turnDTO.Turn_CheckIn = turnDate; 

            var httpContext = HttpContext;
            string jwt = httpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            user = tokenManager.TokenCredentialsGetter(jwt);

            turnDTO.Turn_ClientEmail = user.User_Email;
            try
            {
                string turnCode = await turnDAO.InsertTurn(turnDTO);

                return Ok($"Turno reservado.\nCodigo de turno: {turnCode} ");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrio un error mientras se reservaba el turno.");
            }
        }

        [Authorize(policy: "Client")]
        [HttpGet]
        [Route("/ClientBookedTurns/{turnDate}")]
        public ActionResult ClientBookedTurns( DateTime turnDate )
        {
            //turnDTO.Turn_CheckIn = turnDate;

            var httpContext = HttpContext; 
            string jwt = httpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            user = tokenManager.TokenCredentialsGetter(jwt);

            turnDTOList = turnDAO.ReservedTurnList_ForClients( turnDate, user.User_Email);

            return Ok(turnDTOList);
        }

        [Authorize(policy: "Employee")]
        [HttpGet]
        [Route("/EmployeeBookedTurns/{turnDate}")]
        public ActionResult EmployeeBookedTurns(DateTime turnDate)
        {
            turnDTO.Turn_CheckIn = turnDate;

            var httpContext = HttpContext;
            string jwt = httpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            user = tokenManager.TokenCredentialsGetter(jwt);

            turnDTOList = turnDAO.ReservedTurnList(user.User_Email, turnDTO);

            if (turnDTOList != null) { return Ok(turnDTOList); }

            else { return Ok("No hay turnos reservados."); }
        }
    }
}
