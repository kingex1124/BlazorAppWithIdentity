using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.Components.Pages
{
    public partial class Login
    {
        //[SupplyParameterFromForm]
        private LoginModel? loginModel { get; set; } = new();
        [Inject]
        public IHttpContextAccessor HttpContextAccessor { get; set; } = default!;

        protected override void OnInitialized()
        {
            //loginModel = new LoginModel();
            //loginModel.Username = "kevanchen";
            //loginModel.Password = "1qaz@WSX";
        }
        private async Task HandleLogin()
        {
            if (AuthStateProvider is CustomAuthStateProvider customAuthStateProvider)
            {
                try
                {
                    if (customAuthStateProvider.ValidateUser(loginModel.Username, loginModel.Password))
                    {
                        NavigationManager.NavigateTo("/");
                    }
                    else
                    {
                        // 登入失敗處理
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }

        public class LoginModel
        {
            [Required]
            public string Username { get; set; }
            [Required]
            public string Password { get; set; }
        }
    }
}