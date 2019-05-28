using System;

namespace currencyconverter.AuthorizationModule
{
    public class LoginValidator : IValidator
    {
        public bool Validate(string login)
        {
            return !string.IsNullOrEmpty(login);;
        }
    }
}