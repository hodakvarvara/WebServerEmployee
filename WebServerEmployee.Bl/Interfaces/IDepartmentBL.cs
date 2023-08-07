using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServerEmployee.Domain.Entity;
using WebServerEmployee.Domain.Response;

namespace WebServerEmployee.BL.Interfaces
{
    public interface IDepartmentBL
    {
        Task<IBaseResponse<IEnumerable<Department>>> GetAllDepartments(int id); // получение всех отделов, которые есть  в бд
    }
}
