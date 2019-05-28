using System;
using currencyconverter.AuthorizationModule;
using System.Threading;

using UIKit;

namespace currencyconverter.iOS
{
    public partial class ViewController : UIViewController
    {
        const string TEST_LOGIN = "admin";
        const string TEST_PASS = "12345";

        public ViewController(IntPtr handle) : base(handle)
        {
        }


        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

        }

        partial void LoginButton_TouchUpInside(UIButton sender)
        {
            var authSender = new AuthSender(TEST_LOGIN, TEST_PASS);
            var interactor = new AuthorizationInteractor(new LoginValidator(), new PasswordValidator(), authSender);

            var result = interactor.Login(loginField.Text, passwordField.Text).Result;

            var message = GetMessage(result);
            Alert(message);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
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
            UIAlertController alert = UIAlertController.Create("", message, UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, (actionOK) => { }));
            this.PresentViewController(alert, true, null);
        }

    }
}
