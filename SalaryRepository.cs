using ADO.Net.SalaryUpdate.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace TSQL.UC3.Demo
{
    public class SalaryRepository
    {
        //public static string ConnectionString = "Data Source=LAPTOP-OMN3HO5L;Initial Catalog=payroll_service;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static string connectionString = "Server=LAPTOP-OMN3HO5L; Initial Catalog =payroll_service; User ID = PaviGowda; Password=Pavi@1234";
        SqlConnection connection = new SqlConnection(connectionString);

        public int UpdateSalary(SalaryUpdateModel model)
        {
            try
            {
                int salary = 0;
                using (this.connection)
                {
                    SalaryDetailsModel displayModel = new SalaryDetailsModel();
                    SqlCommand command = new SqlCommand("dbo.spUpdateSalary", this.connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", model.SalaryId);
                    command.Parameters.AddWithValue("@month", model.Month);
                    command.Parameters.AddWithValue("@salary", model.EmployeeSalary);
                    command.Parameters.AddWithValue("@EmpId", model.EmployeeId);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            displayModel.EmployeeId = reader.GetInt32(0);
                            displayModel.EmployeeName = reader["EmployeeName"].ToString();
                            displayModel.JobDiscription = reader["JobDiscription"].ToString();

                            displayModel.Month = reader["Month"].ToString();
                            displayModel.SalaryId = reader.GetInt32(4);
                            displayModel.EmployeeSalary = reader.GetInt32(5);
                            Console.WriteLine("EmployeeId={0}\nEmployeeName={1}\nEmployeeSalary={2}\nMonth={3}\nSalaryId={5}\nJobDescription={4}", displayModel.EmployeeId, displayModel.EmployeeName, displayModel.EmployeeSalary, displayModel.Month, displayModel.JobDiscription, displayModel.SalaryId);
                            Console.WriteLine("\n");
                            salary = displayModel.EmployeeSalary;

                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found");
                    }
                    reader.Close();
                    return salary;
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
         public void InsertIntoTwoTables()
        {
            using (connection)
            {
                connection.Open();

                // Enlist a command in the current transaction.
                SqlCommand command = connection.CreateCommand();

                try
                {
                    // Execute two separate commands.
                    command.CommandText =
                      "insert into Employee(EmployeeName,JobDiscription) values('ZZZZ','Test')";
                    command.ExecuteScalar();
                    Console.WriteLine("Inserted into Employee table successfully.");
                    command.CommandText =
                      "insert into Salary(EmployeeSalary,Month1,EmployeeId) values(1234,'June',1)";
                    command.ExecuteNonQuery();

                    Console.WriteLine("Inserted into Salary table successfully.");
                }
                catch (Exception ex)
                {
                    // Handle the exception if the transaction fails to commit.
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void InsertIntoTwoTablesWithTransactions()
        {
            using (connection)
            {
                connection.Open();

                // Start a local transaction.
                SqlTransaction sqlTran = connection.BeginTransaction();

                // Enlist a command in the current transaction.
                SqlCommand command = connection.CreateCommand();
                command.Transaction = sqlTran;

                try
                {                    
                    // Execute two separate commands.
                    command.CommandText =
                      "insert into Employee(EmployeeName,JobDiscription) values('TesingRollBack','Test')";
                    command.ExecuteScalar();
                    command.CommandText =
                      "insert into Salary(EmployeeSalary,Month1,EmployeeId) values(1234,'July',1)";
                    command.ExecuteNonQuery();

                    // Commit the transaction.
                    sqlTran.Commit();
                    Console.WriteLine("Both records were written to database.");
                }
                catch (Exception ex)
                {
                    // Handle the exception if the transaction fails to commit.
                    Console.WriteLine(ex.Message);

                    try
                    {
                        // Attempt to roll back the transaction.
                        sqlTran.Rollback();
                    }
                    catch (Exception exRollback)
                    {
                        // Throws an InvalidOperationException if the connection
                        // is closed or the transaction has already been rolled
                        // back on the server.
                        Console.WriteLine(exRollback.Message);
                    }
                }
            }
        }
    }
}
