using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using WebServerEmployee.BL.Interfaces;
using WebServerEmployee.DAL.Interfaces;
using WebServerEmployee.DAL.Repositories;
using WebServerEmployee.Domain.Entity;
using WebServerEmployee.Domain.Enum;
using WebServerEmployee.Domain.Response;
using WebServerEmployee.Domain.ViewModel;

namespace WebServerEmployee.BL.Implementations
{
    /// <summary>
    /// Класс служащий "Прослойкой" 
    /// между БД и отображением данных в контроллере
    /// </summary>
    public class EmployeeBL : IEmployeeBL
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeBL(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        /// <summary>
        /// Получение всех сотрудников, которые есть в бд
        /// </summary>
        /// <returns></returns>
        public async Task<IBaseResponse<IEnumerable<Employee>>> GetAllEmployees()
        {
            var baseResponse = new BaseResponse<IEnumerable<Employee>>();
            try 
            {
                var employee = await _employeeRepository.Select();
                if(employee.Count == 0)
                {
                    baseResponse.Description = "Сотрудники не найдены!";
                    baseResponse.StatusCode = StatusCode.ObjNotFound;
                    return baseResponse;
                }
                baseResponse.Data = employee;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Employee>>()
                {
                    Description = $"[GetAllEmployees] : ex.Message",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        /// <summary>
        /// Получение сотрудника по id
        /// </summary>
        /// <returns></returns>
        public async Task<IBaseResponse<Employee>> GetEmployee(int id)
        {
            var baseResponse = new BaseResponse<Employee>();
            try
            {
                var employee = await _employeeRepository.Get(id);
                if (employee == null)
                {
                    baseResponse.Description = "Сотрудник не найден!";
                    baseResponse.StatusCode = StatusCode.ObjNotFound;
                    return baseResponse;
                }
                baseResponse.Data = employee;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Employee>()
                {
                    Description = $"[GetEmployee] : ex.Message",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        /// <summary>
        /// Получение сотрудника по name
        /// </summary>
        /// <returns></returns>
        public async Task<IBaseResponse<Employee>> GetEmployeeByName(string name)
        {
            var baseResponse = new BaseResponse<Employee>();
            try
            {
                var employee = await _employeeRepository.GetByName(name);
                if (employee == null)
                {
                    baseResponse.Description = "Сотрудник не найден!";
                    baseResponse.StatusCode = StatusCode.ObjNotFound;
                    return baseResponse;
                }
                baseResponse.Data = employee;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Employee>()
                {
                    Description = $"[GetEmployeeByName] : ex.Message",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        /// <summary>
        /// Удаление сотрудника по id
        /// </summary>
        /// <returns></returns>
        public async Task<IBaseResponse<bool>> DeleteEmployee(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                // Проверяем существует ли такой сотрудник
                var employee = await _employeeRepository.Get(id);
                if (employee == null)
                {
                    baseResponse.Description = "Сотрудник не найден!";
                    baseResponse.StatusCode = StatusCode.ObjNotFound;
                    return baseResponse;
                }
                await _employeeRepository.Delete(id); // удаляем 
                baseResponse.StatusCode= StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteEmployee] : ex.Message",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }


        /// <summary>
        /// Добавление нового сотрудника 
        /// </summary>
        /// <returns></returns>
        public async Task<IBaseResponse<EmployeeViewModel>> CreateEmployee(EmployeeViewModel employeeViewModel)
        {
            var baseResponse = new BaseResponse<EmployeeViewModel>();
            try
            {
                var employee = new Employee()
                {
                    EmployeeID = employeeViewModel.EmployeeID,
                    Name = employeeViewModel.Name,
                    Surname = employeeViewModel.Surname,
                    Phone_number = employeeViewModel.Phone_number,
                    CompanyID = employeeViewModel.CompanyID,
                    PassportID = employeeViewModel.PassportID,
                    DepartmentID = employeeViewModel.DepartmentID

                };

                var id = await _employeeRepository.Create(employee);
                EmployeeViewModel obj = new EmployeeViewModel()
                {
                    EmployeeID = employee.EmployeeID,
                    Name = employee.Name,
                    Surname = employee.Surname,
                    Phone_number = employee.Phone_number,
                    CompanyID = employee.CompanyID,
                    PassportID = employee.PassportID,
                    DepartmentID = employee.DepartmentID
                };
                baseResponse.Data = obj;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<EmployeeViewModel>()
                {
                    Description = $"[CreateEmployee] : ex.Message",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        /// <summary>
        /// Изменение пользователя
        /// </summary>
        /// <param name="employeeViewModel"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IBaseResponse<Employee>> EditEmployee(EmployeeViewModel employeeViewModel)
        {
            var baseResponse = new BaseResponse<Employee>();
            try
            {
                var employee = await _employeeRepository.Get(employeeViewModel.EmployeeID);
                if (employee == null)
                {
                    baseResponse.StatusCode = StatusCode.ObjNotFound;
                    baseResponse.Description = "Employee not found";
                    return baseResponse;
                }
                else
                {
                    employee.Name = employeeViewModel.Name;
                    employee.Surname = employeeViewModel.Surname;
                    employee.Phone_number = employeeViewModel.Phone_number;
                    employee.CompanyID = employeeViewModel.CompanyID;
                    employee.PassportID = employeeViewModel.PassportID;
                    employee.DepartmentID = employeeViewModel.DepartmentID; 
                    
                    await _employeeRepository.Update(employee);

                    return baseResponse;
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<Employee>()
                {
                    Description = $"[Edit] : ex.Message",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        /// <summary>
        /// Получение сотрудников  по компании
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IBaseResponse<IEnumerable<Employee>>> GetEmployeeByCompany(int CompanyID)
        {
            var baseResponse = new BaseResponse<IEnumerable<Employee>>();
            try
            {
                var employee = await _employeeRepository.SelectEmployeeByCompany(CompanyID);
                if (employee.Count == 0)
                {
                    baseResponse.Description = "Сотрудники не найдены!";
                    baseResponse.StatusCode = StatusCode.ObjNotFound;
                    return baseResponse;
                }
                baseResponse.Data = employee;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Employee>>()
                {
                    Description = $"[GetEmployeeByCompany] : ex.Message",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        /// <summary>
        /// Получение сотрудников по отделу
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IBaseResponse<IEnumerable<Employee>>> GetEmployeeByDepartment(int DepartmentID)
        {
            var baseResponse = new BaseResponse<IEnumerable<Employee>>();
            try
            {
                var employee = await _employeeRepository.SelectEmployeeByDepartment(DepartmentID);
                if (employee.Count == 0)
                {
                    baseResponse.Description = "Сотрудники не найдены!";
                    baseResponse.StatusCode = StatusCode.ObjNotFound;
                    return baseResponse;
                }
                baseResponse.Data = employee;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Employee>>()
                {
                    Description = $"[GetEmployeeByDepartment] : ex.Message",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        public Task<IBaseResponse<bool>> DeleteEmployeeByID()
        {
            throw new NotImplementedException();
        }
    }
}
