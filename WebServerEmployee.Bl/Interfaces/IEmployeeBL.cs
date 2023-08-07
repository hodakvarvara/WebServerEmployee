using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServerEmployee.BL.Implementations;
using WebServerEmployee.Domain.Entity;
using WebServerEmployee.Domain.Response;
using WebServerEmployee.Domain.ViewModel;

namespace WebServerEmployee.BL.Interfaces
{
    public interface IEmployeeBL
    {
        Task<IBaseResponse<IEnumerable<Employee>>> GetAllEmployees(); // получение всех сотрудников, которые есть  в бд
        Task<IBaseResponse<Employee>> GetEmployee(int id); // Получение одного сотрудника по ID
        Task<IBaseResponse<Employee>> GetEmployeeByName(string name); // Получение одного сотрудника по name
        Task<IBaseResponse<bool>> DeleteEmployeeByID();
        Task<IBaseResponse<bool>> DeleteEmployee(int id);
        Task<IBaseResponse<EmployeeViewModel>> CreateEmployee(EmployeeViewModel employeeViewModel);
        Task<IBaseResponse<Employee>> EditEmployee(EmployeeViewModel employeeViewModel);

        Task<IBaseResponse<IEnumerable<Employee>>> GetEmployeeByCompany(int id); // Получение сотрудников  по компании
        Task<IBaseResponse<IEnumerable<Employee>>> GetEmployeeByDepartment(int id); // Получение сотрудников по отделу
    }
}
