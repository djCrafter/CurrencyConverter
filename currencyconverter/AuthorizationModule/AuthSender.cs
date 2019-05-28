using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace currencyconverter.AuthorizationModule
{
    public class AuthSender : IAuthSender
    {
        private string login;
        private string pass;

        public AuthSender(string login, string pass)
        {
            this.login = login;
            this.pass = pass;
        }

        public async Task<bool> SendAuthRequest(string login, string pass)
        {
            if (login == this.login && pass == this.pass)
                return true;
       
            return false;
        }
    }
}
