namespace BlazorApp2.Components.Pages
{
    public partial class Counter
    {
        private int currentCount = 0;

        private void IncrementCount()
        {
            currentCount++;
        }

        private LoginModel loginModel = new();
        private string? message;

        private async Task LoginAsync()
        {
            // 模擬非同步登入處理
            await Task.Delay(1000);

            if (string.IsNullOrWhiteSpace(loginModel.Username) || string.IsNullOrWhiteSpace(loginModel.Password))
            {
                message = "請輸入帳號與密碼";
            }
            else if (loginModel.Username == "admin" && loginModel.Password == "1234")
            {
                message = "登入成功！";
            }
            else
            {
                message = "帳號或密碼錯誤";
            }
        }

        public class LoginModel
        {

            public string? Username { get; set; }

            public string? Password { get; set; }
        }
    }
}