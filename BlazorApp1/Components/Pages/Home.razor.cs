using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorApp1.Components.Pages
{
    public partial class Home
    {
        private AuthenticationState authState;
        private List<string> userRoles = new List<string>();

        protected override async Task OnInitializedAsync()
        {
            authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();

            if (authState.User.Identity?.IsAuthenticated == true)
            {
                userRoles = authState.User.Claims
                    .Where(c => c.Type == System.Security.Claims.ClaimTypes.Role)
                    .Select(c => c.Value)
                    .ToList();
            }
        }
    }
}