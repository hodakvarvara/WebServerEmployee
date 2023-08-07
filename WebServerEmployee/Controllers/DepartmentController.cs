using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebServerEmployee.BL.Interfaces;

namespace WebServerEmployee.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentBL _departmentBL;
        public DepartmentController(IDepartmentBL departmentBL)
        {
            _departmentBL = departmentBL;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDepartments(int id)
        {
            var response = await _departmentBL.GetAllDepartments(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data); 
            }
            return RedirectToAction("MyError", "MyError", response); // переход на представление, которое будет отображать ошибку
        }
    }
}
