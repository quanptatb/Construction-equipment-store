using Microsoft.AspNetCore.Mvc;

namespace VietMachWeb.Controllers.Admin
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
