namespace BlazorApp1.Components.Pages
{
    public partial class TESTSample
    {
        private Model _model = new();
        private bool _isDisabled;

        private void OnDisable()
        {
            _isDisabled = !_isDisabled;
        }

        public class Model
        {
            public string? Name { get; set; }
            public string? Value { get; set; }
        }
    }
}