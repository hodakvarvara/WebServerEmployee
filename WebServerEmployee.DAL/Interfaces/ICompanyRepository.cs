using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServerEmployee.Domain.Entity;

namespace WebServerEmployee.DAL.Interfaces
{
    public interface ICompanyRepository<T>
    {
        Task<List<T>> SelectCompany(); // получение всех объектоv
       
    }
}
