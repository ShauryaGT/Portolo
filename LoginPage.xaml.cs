using MAGIApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MAGIApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPageViewModel loginPageViewModel;
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = loginPageViewModel = new LoginPageViewModel();
            loginPageViewModel.isChecked = true;
        }

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value == true)
            {
                loginPageViewModel.isChecked = true;
            }
            else
            {
                loginPageViewModel.isChecked = false;
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            if (loginPageViewModel.isChecked == false)
            {
                loginPageViewModel.isChecked = true;
            }
            else
            {
                loginPageViewModel.isChecked = false;
            }
        }
    }
}