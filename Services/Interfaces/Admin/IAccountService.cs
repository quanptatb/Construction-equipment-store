using VietMachWeb.Models.DTOs.Admin;

namespace VietMachWeb.Services.Interfaces.Admin
{
    public interface IAccountService
    {
        Task<bool> LoginAsync(LoginRequestDTO request, HttpContext httpContext);
        Task LogoutAsync(HttpContext httpContext);
    }
}
