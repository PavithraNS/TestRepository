create procedure dbo.SpAddEmployeeDetails
(	
	@name	varchar(150),		
	@Base_Pay float,		
	@start	date,		
	@gender	char(1),	
	@phone_number	int,		
	@address	varchar(150),
	@department	varchar(150),
	@Deductions	float,	
	@Taxable_pay float,		
	@Net_pay	float,		
	@Income_tax	float		
	)
	as begin
	Insert into employee_payroll values(@name,@Base_Pay,@start,@gender,@phone_number,@address,@department,@Deductions,@Taxable_pay,@Net_pay,@Income_tax)
	End