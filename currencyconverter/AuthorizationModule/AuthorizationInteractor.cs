using System;
using System.Threading.Tasks;

namespace currencyconverter.AuthorizationModule
{
    public class AuthorizationInteractor : IAuthorizationInteractor
    {
        private readonly IValidator _passwordValidator;
        private readonly IValidator _loginValidator;
        private readonly IAuthSender _authSender;


        public AuthorizationInteractor(IValidator loginValidator, IValidator passwordValidator, IAuthSender authSender)
        {
            _loginValidator = loginValidator ?? throw new ArgumentNullException(nameof(loginValidator));
            _authSender = authSender ?? throw new ArgumentNullException(nameof(authSender));
            _passwordValidator = passwordValidator ?? throw new ArgumentNullException(nameof(passwordValidator));
        }

        public async Task<EAuthResult> Login(string login, string pass)
        {
            EAuthResult res;
            if (!_loginValidator.Validate(login) | !_passwordValidator.Validate(pass))
            {
                res = EAuthResult.InvalidData;
            }
            else
            {
                res = await GetSenderResult(login, pass);
            }

            return res;
        }

        private async Task<EAuthResult> GetSenderResult(string login, string pass)
        {
            EAuthResult res;
            if (await _authSender.SendAuthRequest(login, pass))
            {
                res = EAuthResult.Success;
            }
            else
            {
                res = EAuthResult.Unauthorized;
            }

            return res;
        }
    }
}