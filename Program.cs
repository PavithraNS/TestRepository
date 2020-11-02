using System;

namespace ADO.NetDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            EmployeeRepository repository = new EmployeeRepository();
           // repository.GetAllEmployees();

            EmployeeModel model = new EmployeeModel();
            model.EmployeeName = "Ramya";
            model.Address = "Hyderabad";
            model.BasicPay = 45;
            model.Deductions = 454;
            model.Department = "IT";
            model.Gender = "F";
            model.PhoneNumber = 983798;
            model.NetPay = 833;
            model.Tax = 32;
            model.StartDate = DateTime.Now;
            model.TaxablePay = 324;

            Console.WriteLine(repository.AddEmployee(model)? "Record inserted successfully " :"Failed"); 
            Console.ReadLine();
        }
    }
}
