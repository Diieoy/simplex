using Caliburn.Micro;

namespace WpfApp.ViewModels
{
    public class ErrorViewModel : Screen
    {
        public static string ErrorMessage { get; set; }

        public ErrorViewModel(string message)
        {
            ErrorMessage = message;
        }

        public void Close()
        {
            TryClose();
        }
    }
}
