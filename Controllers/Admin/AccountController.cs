using Microsoft.AspNetCore.Mvc;
using VietMachWeb.Models.DTOs.Admin;
using VietMachWeb.Services.Interfaces.Admin;

namespace VietMachWeb.Controllers.Admin
{
    [Route("admin")]
    public class AccountController : Controller
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService service)
        {
            _service = service;
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View(); // Razor View
        }

        [HttpPost("login")]
        [ValidateAntiForgeryToken] // CSRF
        public async Task<IActionResult> Login(LoginRequestDTO request)
        {
            var success = await _service.LoginAsync(request, HttpContext);

            if (!success)
            {
                ModelState.AddModelError("", "Sai tài khoản hoặc mật khẩu");
                return View();
            }

            return Redirect("/admin/dashboard");
        }

        [HttpPost("logout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _service.LogoutAsync(HttpContext);
            return Redirect("/admin/login");
        }
    }
}
