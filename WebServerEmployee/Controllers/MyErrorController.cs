using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebServerEmployee.Domain.Entity;
using WebServerEmployee.Domain.Enum;
using WebServerEmployee.Domain.Response;

namespace WebServerEmployee.Controllers
{
    public class MyErrorController : Controller
    {
        // GET: ErrorController
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MyError(BaseResponse<Employee> Response)
        {
            return View(Response);
        }
    }
}
