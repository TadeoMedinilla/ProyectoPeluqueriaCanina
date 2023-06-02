using DataAccess.Entities;
using DataAccess.Entities.DTOs;
using DataAccess.Utilities;
using DataAccess.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class EmployeeDAO : SQL_Methods<Employee>
    {
        private Automapper Mapper { get; set; } = new Automapper();

        private Employee Emp { get; set; } = new Employee();
        private List<Employee> EmpList { get; set; } = new List<Employee>();
        private EmployeeDTO EmpDTO { get; set; } = new EmployeeDTO();
        private List<EmployeeDTO> EmpDTOList { get; set; } = new List<EmployeeDTO> ();


        // Insert Employee querys:
        private string EmployeeMaster_Insert { get; } = "INSERT INTO PeluqueriaCanina.dbo.EmployeeMaster ( EmpM_Name, EmpM_LastName)\r\nVALUES ( @Emp_Name, @Emp_LastName);";
        private string EmployeeDetail_Insert { get; } = "SET IDENTITY_INSERT PeluqueriaCanina.dbo.EmployeeDetail ON;\r\nINSERT INTO PeluqueriaCanina.dbo.EmployeeDetail (EmpD_ID, EmpD_DNI, EmpD_Adress, EmpD_Email, EmpD_Role, EmpD_Status)\r\nSELECT EmpM_ID, @Emp_DNI, @Emp_Adress,@Emp_Email, @Emp_Role, @Emp_Status\r\nFROM PeluqueriaCanina.dbo.EmployeeMaster\r\nWHERE EmpM_Name = @Emp_Name AND EmpM_LastName = @Emp_LastName;\r\nSET IDENTITY_INSERT PeluqueriaCanina.dbo.EmployeeDetail OFF;\r\n";

        // Select Employee querys:
        private string EmployeeDetail_SelectByID { get; } =@"SELECT EmpM_ID as Emp_ID, EmpM_Name as Emp_Name, EmpM_LastName as Emp_LastName, EmpD_CheckIn as Emp_CheckIn,
                                                            EmpD_DNI as Emp_DNI, EmpD_Adress as Emp_Adress, EmpD_Email as Emp_Email, EmpD_Role as Emp_Role, EmpD_Status as Emp_Status
                                                            FROM PeluqueriaCanina.dbo.EmployeeDetail
                                                            FULL JOIN PeluqueriaCanina.dbo.EmployeeMaster ON EmpD_ID = EmpM_ID
                                                            WHERE EmpD_ID = @Emp_ID AND EmpD_Status = @Emp_Status";

        private string EmployeeDetail_SelectByEmail { get; } = @"SELECT EmpM_ID as Emp_ID, EmpM_Name as Emp_Name, EmpM_LastName as Emp_LastName, EmpD_CheckIn as Emp_CheckIn,
                                                            EmpD_DNI as Emp_DNI, EmpD_Adress as Emp_Adress, EmpD_Email as Emp_Email, EmpD_Role as Emp_Role, EmpD_Status as Emp_Status
                                                            FROM PeluqueriaCanina.dbo.EmployeeDetail
                                                            FULL JOIN PeluqueriaCanina.dbo.EmployeeMaster ON EmpD_ID = EmpM_ID
                                                            WHERE EmpD_Email = @Emp_Email AND EmpD_Status = @Emp_Status";

        private string EmployeeMaster_Select { get; } = @"SELECT EmpM_ID as emp_ID, EmpM_Name as emp_Name, EmpM_LastName as emp_LastName
                                                        FROM PeluqueriaCanina.dbo.EmployeeMaster";
        private string EmployeeDetail_Select { get; } = @"SELECT EmpM_ID as emp_ID, EmpM_Name as Emp_Name, EmpM_LastName as Emp_LastName, EmpD_CheckIn as Emp_CheckIn,
                                                            EmpD_DNI as Emp_DNI, EmpD_Adress as Emp_Adress, EmpD_Email as Emp_Email, EmpD_Role as Emp_Role, EmpD_Status as Emp_Status
                                                            FROM PeluqueriaCanina.dbo.EmployeeDetail
                                                            FULL JOIN PeluqueriaCanina.dbo.EmployeeMaster ON EmpD_ID = EmpM_ID";
        //Methods:

        public async Task<int> RegisterEmployee(Register empRegistration)
        {
            Emp = Mapper.mapper.Map<Employee>(empRegistration.Reg_Employee);

            int affectedRows_1 = await SQL_Executable(EmployeeMaster_Insert, Emp);

            int affectedRows_2 = await SQL_Executable(EmployeeDetail_Insert, Emp);

            if (affectedRows_1 != 0 && affectedRows_2 != 0) { return 1; }
            else { return 0; }
        }

        public List<EmployeeDTO> GetEmployees()
        {
             EmpList = SQL_Query(EmployeeDetail_Select);

            foreach (Employee emp in EmpList)
            {
                EmpDTO = Mapper.mapper.Map<EmployeeDTO>(emp);
                EmpDTOList.Add(EmpDTO);
            }

            return EmpDTOList;
        }

        public Employee SelectEmployeeByID(Employee emp)
        {
            Emp = SQL_QueryFirstOrDefault(EmployeeDetail_SelectByID,emp);
            
            return Emp;
        }

        public Employee SelectEmployeeByEmail(Employee emp)
        {
            Emp = SQL_QueryFirstOrDefault(EmployeeDetail_SelectByEmail, emp);

            return Emp;
        }
    }
}
