using Azure;
using Microsoft.AspNetCore.Mvc;
using WebServerEmployee.BL.Implementations;
using WebServerEmployee.BL.Interfaces;
using WebServerEmployee.DAL.Interfaces;
using WebServerEmployee.Domain.Entity;
using WebServerEmployee.Domain.Response;
using WebServerEmployee.Domain.ViewModel;

namespace WebServerEmployee.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeBL _employeeBL;
        public EmployeeController(IEmployeeBL employeeBL)
        {
            _employeeBL = employeeBL;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var response = await _employeeBL.GetAllEmployees();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("MyError", "MyError", response); // переход на представление, которое будет отображать ошибку

        }

        [HttpGet]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var response = await _employeeBL.GetEmployee(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("MyError", "MyError", response);// переход на представление, которое будет отображать ошибку

        }

        [HttpGet]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (id == 0)
            {
                return View();
            }
            var response = await _employeeBL.DeleteEmployee(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetAllEmployees"); // преход на представление, отображающее всех сотрудников
            }
            return RedirectToAction("MyError", "MyError", response); // переход на представление, которое будет отображать ошибку
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEmployee(Employee employee)
        {
            if(employee.EmployeeID != 0)
            {
                var response = await _employeeBL.DeleteEmployee((employee.EmployeeID));
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return RedirectToAction("GetAllEmployees"); // преход на представление, отображающее всех сотрудников
                }
            }
           
            return RedirectToAction("MyError", "MyError", new BaseResponse<Employee> { Description = "Сотрудник с таким Id не найден", StatusCode = Domain.Enum.StatusCode.ObjNotFound });
        }


        // добавление/изменение пользователей
        [HttpGet]
        public async Task<IActionResult> Save(int id)
        {

            // если == 0, то это добавление нового объекта в бд
            if (id == 0)
            {
                return View();
            }
            // иначе изменение имеющегося объекта
            var response = await _employeeBL.GetEmployee(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data); // преход на представление, отображающее всех сотрудников
            }
            return RedirectToAction("MyError", "MyError", response); // переход на представление, которое будет отображать ошибку
        }

        [HttpPost]
        public async Task<IActionResult> Save(EmployeeViewModel employeeViewModel)
        {
            if (employeeViewModel.EmployeeID == 0)
            {
                 var response = await _employeeBL.CreateEmployee(employeeViewModel); // создаем нового сотрудника

                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return RedirectToAction("GetEmployee", new { id = response.Data.EmployeeID });
                }
                return RedirectToAction("MyError", "MyError", response);

            }
            else
            {
                var response = await _employeeBL.EditEmployee(employeeViewModel); // изменяем сотрудника
                
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return RedirectToAction("GetEmployee", new { id = employeeViewModel.EmployeeID });
                }
                return RedirectToAction("MyError", "MyError", response);
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateEmployeeById(int id)
        {
            if (id == 0)
            {
                return View();
            }
            // иначе изменение имеющегося объекта
            var response = await _employeeBL.GetEmployee(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data); // преход на представление, отображающее всех сотрудников
            }
            return RedirectToAction("MyError", "MyError", response); // переход на представление, которое будет отображать ошибку
        }
        //void
        [HttpPost]
        public async Task<IActionResult> UpdateEmployeeById(Employee employee)
        {
            return  RedirectToAction("Save", new { id = employee.EmployeeID });
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeeByCompany(int id, Employee employee)
        {
            var response = await _employeeBL.GetEmployeeByCompany(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data); // преход на представление, отображающее всех сотрудников
            }
            return RedirectToAction("MyError", "MyError",  response ); // переход на представление, которое будет отображать ошибку
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeeByDepartment(int id)
        {
            var response = await _employeeBL.GetEmployeeByDepartment(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);// преход на представление, отображающее всех сотрудников
            }
            return RedirectToAction("MyError", "MyError", response); // переход на представление, которое будет отображать ошибку
        }
    }
}
