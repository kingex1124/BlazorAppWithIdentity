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
            // �����D�P�B�n�J�B�z
            await Task.Delay(1000);

            if (string.IsNullOrWhiteSpace(loginModel.Username) || string.IsNullOrWhiteSpace(loginModel.Password))
            {
                message = "�п�J�b���P�K�X";
            }
            else if (loginModel.Username == "admin" && loginModel.Password == "1234")
            {
                message = "�n�J���\�I";
            }
            else
            {
                message = "�b���αK�X���~";
            }
        }

        public class LoginModel
        {

            public string? Username { get; set; }

            public string? Password { get; set; }
        }
    }
}