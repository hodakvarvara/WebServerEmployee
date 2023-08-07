using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServerEmployee.Domain.Entity;

namespace WebServerEmployee.DAL.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<T> Get(int id); // получение одного объекта 
        Task<List<T>> Select(); // получение всех объектов
        Task<bool> Delete(int id); 
        Task<int> Create(T entity);
        Task<T> Update(T entity);
    }
}
