select *  from PeluqueriaCanina.dbo.EmployeeMaster
Select *   from PeluqueriaCanina.dbo.EmployeeDetail
Select *  from PeluqueriaCanina.dbo.UserMaster


Delete   from PeluqueriaCanina.dbo.EmployeeDetail
Delete  from PeluqueriaCanina.dbo.UserMaster
Delete  from PeluqueriaCanina.dbo.EmployeeMaster

--1) Insertar un registro en la tabla UserMaster
INSERT INTO PeluqueriaCanina.dbo.UserMaster (UserM_Email, UserM_Role)
VALUES ('john@example.com', 1);

--INSERT INTO PeluqueriaCanina.dbo.UserMaster (UserM_Email,UserM_HashedPass, UserM_Role)
--VALUES (@User_Email,@User_HashedPass, @User_Role);

--2) Insertar un registro en la tabla EmployeeMaster:
INSERT INTO PeluqueriaCanina.dbo.EmployeeMaster ( EmpM_Name, EmpM_LastName)
VALUES ( 'John', 'Doe');

--INSERT INTO PeluqueriaCanina.dbo.EmployeeMaster ( EmpM_Name, EmpM_LastName)
--VALUES ( @Emp_Name, @Emp_LastName);

--3) Insertar un registro en la tabla EmployeeDetail
SET IDENTITY_INSERT PeluqueriaCanina.dbo.EmployeeDetail ON;
INSERT INTO PeluqueriaCanina.dbo.EmployeeDetail (EmpD_ID, EmpD_DNI, EmpD_Adress, EmpD_Email, EmpD_Role, EmpD_Status)
SELECT EmpM_ID, 123456789, '123 Main St', 'john@example.com', 1, 1
FROM PeluqueriaCanina.dbo.EmployeeMaster
WHERE EmpM_Name = 'John' AND EmpM_LastName = 'Doe';
SET IDENTITY_INSERT PeluqueriaCanina.dbo.EmployeeDetail OFF;

--SET IDENTITY_INSERT PeluqueriaCanina.dbo.EmployeeDetail ON;
--INSERT INTO PeluqueriaCanina.dbo.EmployeeDetail (EmpD_ID, EmpD_DNI, EmpD_Adress, EmpD_Email, EmpD_Role, EmpD_Status)
--SELECT EmpM_ID, @Emp_DNI, @Emp_Adress,@Emp_Email, @Emp_Role, @Emp_Status
--FROM PeluqueriaCanina.dbo.EmployeeMaster
--WHERE EmpM_Name = @Emp_Name AND EmpM_LastName = @Emp_LastName;
--SET IDENTITY_INSERT PeluqueriaCanina.dbo.EmployeeDetail OFF;

select * from PeluqueriaCanina.dbo.RoleDetail
select * from PeluqueriaCanina.dbo.EmployeeStatus

Insert into PeluqueriaCanina.dbo.RoleDetail (RoleD_ID, RoleD_Description)
Values (2, 'Employee')

--Select a UserMaster para LogIn
SELECT UserM_Email, UserM_HashedPass, UserM_Role
FROM PeluqueriaCanina.dbo.UserMaster
WHERE UserM_Email = 'Tadeo@mail.com'

--SELECT UserM_Email, UserM_HashedPass, UserM_Role
--FROM PeluqueriaCanina.dbo.UserMaster
--WHERE UserM_Email = @User_Email