using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServerEmployee.Domain.Entity;
using WebServerEmployee.Domain.Response;

namespace WebServerEmployee.BL.Interfaces
{
    public interface ICompanyBL
    {
        Task<IBaseResponse<IEnumerable<Company>>> GetAllCompanies(); // получение всех company, которые есть  в бд
      
    }
}
