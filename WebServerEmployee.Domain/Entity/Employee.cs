using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServerEmployee.Domain.Entity
{
    public class Employee
    {
        public int EmployeeID {  get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone_number { get; set; }
        public int CompanyID { get; set; }
        public int PassportID { get; set; }
        public int DepartmentID { get; set; }
        public virtual Company Company { get; set; }
        //public virtual Department Department { get; set; } // создание объекта Department

    }
}
