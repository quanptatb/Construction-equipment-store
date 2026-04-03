using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using VietMachWeb.Data;
using Microsoft.EntityFrameworkCore;
using VietMachWeb.Services.Interfaces.Admin;
using VietMachWeb.Models.DTOs.Admin;

namespace VietMachWeb.Services.Implementations.Admin
{
    public class AccountService : IAccountService
    {
        private readonly AppDbContext _context;

        public AccountService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> LoginAsync(LoginRequestDTO request, HttpContext httpContext)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(x => x.Email == request.Email);

            if (user == null)
                return false;

            //  BCrypt verify
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return false;

            //  Claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim("UserId", user.Id.ToString())
            };

            foreach (var role in user.UserRoles.Select(x => x.Role.Name))
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var identity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            await httpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal);

            return true;
        }

        public async Task LogoutAsync(HttpContext httpContext)
        {
            await httpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
