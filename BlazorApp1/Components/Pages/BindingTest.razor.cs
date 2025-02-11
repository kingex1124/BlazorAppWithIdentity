using Microsoft.AspNetCore.Components;

namespace BlazorApp1.Components.Pages
{
    public partial class BindingTest
    {
        private LoginModel loginModel = new LoginModel();
        [SupplyParameterFromForm]
        public LoginModel? test2 { get; set; } = new ();
        //protected override void OnInitialized()
        //{
        //    loginModel = new LoginModel();
        //    loginModel.Username = "kevanchen";
        //    loginModel.Password = "1qaz@WSX";
        //}
        private void OnDisable()
        {

        }
        private async Task HandleLogin()
        {
            loginModel.Username = "kevanchen";
            loginModel.Password = "1qaz@WSX";
            //if (AuthStateProvider is CustomAuthStateProvider customAuthStateProvider)
            //{
            //    try
            //    {
            //        HttpClient httpClient = new HttpClient();
            //        httpClient.BaseAddress = new Uri("www.google.com.tw");

            //        await httpClient.PostAsJsonAsync("api/Cookies/SetAccessTokenCookies", string.Empty);
            //    }
            //    catch (Exception ex)
            //    {

            //    }


            //    if (customAuthStateProvider.ValidateUser(loginModel.Username, loginModel.Password))
            //    {
            //        NavigationManager.NavigateTo("/");
            //    }
            //    else
            //    {
            //        // 登入失敗處理
            //    }
            //}
        }

        public class LoginModel
        {

            public string? Username { get; set; }

            public string? Password { get; set; }
        }
    }
}