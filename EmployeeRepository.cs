using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ADO.NetDemo
{
    public class EmployeeRepository
    {
        //public static string ConnectionString = "Data Source=LAPTOP-OMN3HO5L;Initial Catalog=payroll_service;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static string connectionString = "Server=LAPTOP-OMN3HO5L; Initial Catalog =payroll_service; User ID = PaviGowda; Password=Pavi@1234";
        SqlConnection connection = new SqlConnection(connectionString);

        public void GetAllEmployees()
        {
            EmployeeModel model = new EmployeeModel();
            try
            {
                using (connection)
                {
                    string query = @"select * from dbo.employee_payroll";
                    SqlCommand command = new SqlCommand(query, connection);
                    this.connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            model.EmployeeID = reader.GetInt32(0);
                            model.EmployeeName = reader.GetString(1);
                            model.BasicPay = reader.GetDouble(2);
                            model.StartDate = reader.GetDateTime(3);
                            model.Gender = reader.GetString(4);
                            model.PhoneNumber = reader.GetInt32(5);
                            model.Address = reader.GetString(6);
                            model.Department = reader.GetString(7);
                            model.Deductions = reader.GetDouble(8);
                            model.TaxablePay = reader.GetDouble(9);
                            model.NetPay = reader.GetDouble(10);
                            model.Tax = reader.GetDouble(11);
                            Console.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", model.EmployeeID, model.EmployeeName, model.Gender, model.Address, model.BasicPay, model.StartDate, model.PhoneNumber, model.Address, model.Department, model.Deductions, model.TaxablePay, model.Tax, model.NetPay);
                            Console.WriteLine("\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found");
                    }
                    reader.Close();
                    //this.connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }

        public bool AddEmployee(EmployeeModel model)
        {
            try
            {
                using (this.connection)
                {
                    SqlCommand command = new SqlCommand("dbo.SpAddEmployeeDetails", this.connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@name", model.EmployeeName);
                    command.Parameters.AddWithValue("@Base_Pay", model.BasicPay);
                    command.Parameters.AddWithValue("@start", model.StartDate);
                    command.Parameters.AddWithValue("@gender", model.Gender);
                    command.Parameters.AddWithValue("@phone_number", model.PhoneNumber);
                    command.Parameters.AddWithValue("@address", model.Address);
                    command.Parameters.AddWithValue("@department", model.Department);
                    command.Parameters.AddWithValue("@Deductions", model.Deductions);
                    command.Parameters.AddWithValue("@Taxable_pay", model.TaxablePay);
                    command.Parameters.AddWithValue("@Net_pay", model.NetPay);
                    command.Parameters.AddWithValue("@Income_tax", model.Tax);
                    connection.Open();
                    var result = command.ExecuteNonQuery();
                    connection.Close();

                    if (result != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }


    }
}
