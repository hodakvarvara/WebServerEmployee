using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServerEmployee.Domain.ViewModel
{
    /// <summary>
    /// Данный класс содержит атрибуты для валидация модели в форме
    /// </summary>
    public class EmployeeViewModel
    {
        public int EmployeeID { get; set; } 
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone_number { get; set; }
        public int CompanyID { get; set; }
        public int PassportID { get; set; }
        public int DepartmentID { get; set; }
    }
}
