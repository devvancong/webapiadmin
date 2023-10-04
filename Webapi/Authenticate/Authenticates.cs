using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebDataModel.BaseClass;
using WebDataModel.ViewModel;
using WebService.Interface;

namespace Webapi.Authenticate
{
    public class Authenticates : IAuthenticate
    {
        public readonly IUserService _userService;
        private readonly IConfiguration _iconfiguration;

        public Authenticates(IUserService userService,
            IConfiguration iconfiguration)
        {
            _userService = userService;
            _iconfiguration = iconfiguration;
        }

        public async Task<Tokens> AuthenticateUser(UserLogin userLogin)
        {
            Tokens tokens = new Tokens();
            try
            {
                var _isCheckLogin = await _userService.Login(userLogin);
                if (_isCheckLogin.Status)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, _isCheckLogin.UserName),
                        new Claim(ClaimTypes.Role, _isCheckLogin.Role)
                    };
                    string accessToken = GenerateAccessToken(claims);
                    string refreshToken = GenerateRefreshToken();

                    tokens.Status = true;
                    tokens.UserName = _isCheckLogin.UserName;
                    tokens.Role = _isCheckLogin.Role;
                    tokens.Token = accessToken;
                    tokens.RefreshToken = refreshToken;
                    tokens.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

                    return tokens;
                }
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
            return tokens;
        }

        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_iconfiguration["JWT:Key"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature);
            var tokeOptions = new JwtSecurityToken(
                issuer: _iconfiguration["JWT:Issuer"],
                audience: _iconfiguration["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_iconfiguration["JWT:Key"])),
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }

        public async Task<AuthenticatedResponse> RefreshToken(TokenApiModel tokenApiModel)
        {
            try
            {
                if (tokenApiModel is null)
                    return new AuthenticatedResponse();

                string accessToken = tokenApiModel.AccessToken;
                string refreshToken = tokenApiModel.RefreshToken;

                ///Sẽ xác thực userName và Refreshtoken
                var principal = GetPrincipalFromExpiredToken(accessToken);
                var username = principal.Identity.Name;
                var user = await _userService.Get(exp => exp.Name == username);
                if (user is null)
                {
                    return new AuthenticatedResponse();
                }

                var newAccessToken = GenerateAccessToken(principal.Claims);
                var newRefreshToken = GenerateRefreshToken();

                return new AuthenticatedResponse()
                {
                    Token = newAccessToken,
                    RefreshToken = newRefreshToken,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }

        
    }
}