using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.Components.Pages
{
    public partial class Login
    {
        [SupplyParameterFromForm]
        private LoginModel? loginModel { get; set; } = new();

        protected override void OnInitialized()
        {
            //loginModel = new LoginModel();
            //loginModel.Username = "kevanchen";
            //loginModel.Password = "1qaz@WSX";
        }
        private async Task HandleLogin()
        {
            loginModel.Username = "kevanchen";
            loginModel.Password = "1qaz@WSX";
            if (AuthStateProvider is CustomAuthStateProvider customAuthStateProvider)
            {
                try
                {
                    HttpClient httpClient = new HttpClient();
                    httpClient.BaseAddress = new Uri("www.google.com.tw");

                    await httpClient.PostAsJsonAsync("api/Cookies/SetAccessTokenCookies", string.Empty);
                }
                catch (Exception ex)
                {

                }


                if (customAuthStateProvider.ValidateUser(loginModel.Username, loginModel.Password))
                {
                    NavigationManager.NavigateTo("/");
                }
                else
                {
                    // 登入失敗處理
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