using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServerEmployee.DAL.Interfaces
{
    public interface IDepartmentRepository<T>
    {
         Task<List<T>> Select(int id); // получение всех объектов
    }
}
