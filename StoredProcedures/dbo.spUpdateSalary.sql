CREATE PROCEDURE dbo.spUpdateSalary
	@id int,	
	@salary int,
	@month varchar(20),
	@EmpId int
AS
BEGIN
SET XACT_ABORT ON;
BEGIN TRY
BEGIN TRANSACTION;
Update Salary
set EmployeeSalary=@salary
where SalaryId=@id and Month=@month and EmployeeId=@EmpId;
select e.EmployeeId,e.EmployeeName,e.JobDiscription,s.Month,s.SalaryId,s.EmployeeSalary
from Employee e inner join Salary s 
ON e.EmployeeId=s.SalaryId where s.SalaryId=@id; 
COMMIT TRANSACTION;
END TRY
BEGIN CATCH
select ERROR_NUMBER() AS ErrorNumber, ERROR_MESSAGE() AS ErrorMessage;
if(XACT_STATE())=-1
BEGIN
PRINT N'The transaction is in an uncommitable state.'+'Rolling back transaction.'
ROLLBACK TRANSACTION;
END;
if(XACT_STATE())=1
BEGIN
PRINT N'The transaction is commitable state.'+'committing transaction.'
COMMIT TRANSACTION;
END;
END CATCH
END