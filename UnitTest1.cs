using Microsoft.VisualStudio.TestTools.UnitTesting;
using ADO.Net.SalaryUpdate;
using ADO.Net.SalaryUpdate.Model;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GivenSalaryDetails_AbleToUpdateSalarayDetails()
        {
            SalaryRepository salary = new SalaryRepository();
            SalaryUpdateModel updateModel = new SalaryUpdateModel()
            {
                SalaryId=2,
                Month="Jan",
                EmployeeSalary=120,
                EmployeeId=1
        };

           int EmpSalary= salary.UpdateSalary(updateModel);

           Assert.AreEqual(updateModel.EmployeeSalary,EmpSalary);
        }
    }
}
