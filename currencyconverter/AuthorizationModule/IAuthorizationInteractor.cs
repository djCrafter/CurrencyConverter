using System.Threading.Tasks;

namespace currencyconverter.AuthorizationModule
{
    public interface IAuthorizationInteractor
    {
        Task<EAuthResult> Login(string login, string pass);
    }
}
