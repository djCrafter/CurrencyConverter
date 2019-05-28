using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace currencyconverter.AuthorizationModule
{
    public class PasswordValidator : IValidator
    {
        public bool Validate(string str)
        {
            return !string.IsNullOrEmpty(str);
        }
    }
}
