using Microsoft.AspNetCore.Mvc;
using WebServerEmployee.BL.Implementations;
using WebServerEmployee.BL.Interfaces;

namespace WebServerEmployee.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyBL _companyBL;
        public CompanyController(ICompanyBL companyBL)
        {
            _companyBL = companyBL;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCompanies()
        {
            var response = await _companyBL.GetAllCompanies();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error"); // переход на представление, которое будет отображать ошибку

        }
        
    }
}
