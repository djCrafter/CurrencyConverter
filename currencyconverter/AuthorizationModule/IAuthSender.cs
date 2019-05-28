using System.Threading.Tasks;

namespace currencyconverter.AuthorizationModule
{
    public interface IAuthSender
    {
        Task<bool> SendAuthRequest(string login, string pass);
    }
}