using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServerEmployee.Domain.Entity
{
    public class Employee
    {
        public int EmployeeID {  get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surname is required")]
        public string Surname { get; set; }

        [Phone(ErrorMessage = "Invalid phone number")]
        public string Phone_number { get; set; }
        public int CompanyID { get; set; }
        public int PassportID { get; set; }
        public int DepartmentID { get; set; }
        public virtual Company Company { get; set; }
        //public virtual Department Department { get; set; } // создание объекта Department

    }
}
