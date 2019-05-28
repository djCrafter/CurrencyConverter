using Android.App;
using Android.Widget;
using Android.OS;
using System.Runtime.Remoting.Contexts;
using Android.Views.InputMethods;
using Android.Views;
using currencyconverter.AuthorizationModule;

namespace currencyconverter.Droid
{
    [Activity(Label = "currencyconverter", MainLauncher = true, Icon = "@mipmap/icon", WindowSoftInputMode = SoftInput.AdjustResize)]
    public class MainActivity : Activity
    {
        const string TEST_LOGIN = "admin";
        const string TEST_PASS = "12345";

        EditText loginField;
        EditText passwordField;
        Button loginButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            loginField = FindViewById<EditText>(Resource.Id.login_field);
            passwordField = FindViewById<EditText>(Resource.Id.password_field);
            loginButton = FindViewById<Button>(Resource.Id.button);

       
            loginButton.Click += LoginButton_Click;
        }

        private async void LoginButton_Click(object sender, System.EventArgs e)
        {         
            var authSender = new AuthSender(TEST_LOGIN, TEST_PASS);
            var interactor = new AuthorizationInteractor(new LoginValidator(), new PasswordValidator(), authSender);

            var result = await interactor.Login(loginField.Text, passwordField.Text);

            var message = GetMessage(result);
            Alert(message);
        }

        private string GetMessage(EAuthResult result)
        {
            switch (result)
            {
                case EAuthResult.InvalidData: return "Username or password is not entered!";
                case EAuthResult.Success: return "Authenticated successfully!";
                case EAuthResult.Unauthorized: return "Incorrect login or password!";
                default: return "";
            }
        }

        private void Alert(string message)
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle("");
            alert.SetMessage(message);
            alert.SetPositiveButton("OK", (senderAlert, args) => { });

            Dialog dialog = alert.Create();
            dialog.Show();
        }
    }
}

