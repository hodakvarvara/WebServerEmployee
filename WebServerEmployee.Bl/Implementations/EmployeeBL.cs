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
                    baseResponse.Description = "Сотрудники не найдены! Не удалось получить список всех сотрудников";
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
                    Description = "Ошибка при выводе всех сотрудников, исключение в методе GetAllEmployees(). \r\n ex.Message: " + ex.Message,
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
                    Description = "Ошибка при выводе  сотрудника по id, исключение в методе GetEmployee(). \r\n ex.Message: " + ex.Message,
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
                    Description = "Ошибка при выводе  сотрудника по Name, исключение в методе GetEmployeeByName. \r\n ex.Message: " + ex.Message,
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
                    baseResponse.Description = "Сотрудник не найден! Не удалось удалить сотрудника";
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
                    Description = "Ошибка при удалении сотрудника, исключение в методе DeleteEmployee(). \r\n ex.Message: " + ex.Message,
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
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<EmployeeViewModel>()
                {
                    Description = "Не удалось добавить сотрудника, исключение в методе CreateEmployee. Не корректно введены данные \r\n ex.Message: " + ex.Message,
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
                    baseResponse.Description = "Сотрудник не найден! Не удалось отредактировать информацию о сотруднике.";
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

                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<Employee>()
                {
                    Description = "Не удалось изменить данные о сотруднике, исключение в методе EditEmployee(). Не корректно введены данные \r\n ex.Message: " + ex.Message,
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
                    baseResponse.Description = "Сотрудники в этой компании не найдены!";
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
                    Description = "Ошибка при выводе сотрудников компании, исключение в методе GetEmployeeByCompany. Не корректно введены данные \r\n ex.Message: " + ex.Message,
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
                    baseResponse.Description = "Сотрудники в этом отделе не найдены!";
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
                    Description = "Ошибка при выводе сотрудников отдела, исключение в методе GetEmployeeByDepartment(). Не корректно введены данные \r\n ex.Message: " + ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
