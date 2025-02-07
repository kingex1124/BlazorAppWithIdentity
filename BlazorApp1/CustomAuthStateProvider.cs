using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BlazorApp1
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomAuthStateProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var authenticateResult = await httpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (authenticateResult.Succeeded)
            {
                return new AuthenticationState(authenticateResult.Principal);
            }

            return new AuthenticationState(new ClaimsPrincipal());
        }

        public bool ValidateUser(string username, string password)
        {
            if (username == "kevanchen" && password == "1qaz@WSX")
            {
                var claims = new[]
                {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "admin"),
                new Claim(ClaimTypes.Role, "Tester")
            };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                var httpContext = _httpContextAccessor.HttpContext;
                httpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    claimsPrincipal,
                    new AuthenticationProperties { IsPersistent = true }
                ).Wait();
                //_httpContextAccessor.HttpContext?.Response.Cookies.Append(
                //    "TEST",
                //    "TESTValue",
                //    new CookieOptions
                //    {
                //        IsEssential = true,
                //        HttpOnly = true,
                //        Secure = true,
                //        Expires = DateTime.UtcNow.AddHours(24),
                //        SameSite = SameSiteMode.Strict
                //    });
                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
                return true;
            }
            return false;
        }

        public void Logout()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();

            NotifyAuthenticationStateChanged(
                Task.FromResult(new AuthenticationState(new ClaimsPrincipal()))
            );
        }
    }
}
