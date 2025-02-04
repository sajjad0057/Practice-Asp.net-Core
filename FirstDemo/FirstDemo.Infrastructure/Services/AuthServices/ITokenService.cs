
using System.Security.Claims;

namespace FirstDemo.Infrastructure.Services
{
    public interface ITokenService
    {
        Task<string> GetJwtToken(IList<Claim> claims);
    }
}