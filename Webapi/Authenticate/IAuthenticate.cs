using System.Security.Claims;
using WebDataModel.BaseClass;
using WebDataModel.ViewModel;

namespace Webapi.Authenticate
{
    public interface IAuthenticate
    {
        Task<Tokens> AuthenticateUser(UserLogin userLogin);
        Task<AuthenticatedResponse> RefreshToken(TokenApiModel tokenApiModel);

        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
