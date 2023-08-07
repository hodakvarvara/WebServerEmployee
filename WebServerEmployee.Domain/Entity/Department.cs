using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServerEmployee.Domain.Entity
{
    public class Department
    {
        public int DepartmentID { get; set; }
        public string Name { get; set; }
        public string Phone_number { get; set; }
        public int CompanyID { get; set; }

    }
}
