using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServerEmployee.Domain.Entity
{
    public class Department
    {
        public int DepartmentID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Phone(ErrorMessage = "Invalid phone number")]
        public string Phone_number { get; set; }
        public int CompanyID { get; set; }

    }
}
