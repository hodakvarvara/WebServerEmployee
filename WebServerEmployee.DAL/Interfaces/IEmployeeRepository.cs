using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServerEmployee.Domain.Entity;

namespace WebServerEmployee.DAL.Interfaces
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        Task<Employee> GetByName(string name); // получение сотрудника по имени
        Task<List<Employee>> SelectEmployeeByCompany(int id); // Получение сотрудников  по компании
        Task<List<Employee>> SelectEmployeeByDepartment(int id); // Получение сотрудников  по отделу
    }
}
