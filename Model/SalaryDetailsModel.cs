using System;
using System.Collections.Generic;
using System.Text;

namespace ADO.Net.SalaryUpdate.Model
{
    class SalaryDetailsModel
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string JobDiscription { get; set; }
        public string Month { get; set; }
        public int EmployeeSalary { get; set; }
        public int SalaryId { get; set; }
    }
}
