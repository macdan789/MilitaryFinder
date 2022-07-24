using MilitaryFinder.API.Domain;
using System.Threading.Tasks;

namespace MilitaryFinder.API.Services.Abstract
{
    public interface IIdentityService
    {
        Task<AuthentificationResult> RegisterAsync(string emailAddress, string password);
        Task<AuthentificationResult> LoginAsync(string emailAddress, string password);
    }
}