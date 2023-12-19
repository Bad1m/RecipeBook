using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AuthMicroservice.BusinessLogic.Interfaces
{
    public interface IJWTService
    {
        SigningCredentials GetSigningCredentials();
        Task<List<Claim>> GetClaims();
        JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims);
        Task<(string accessToken, string refreshToken)> CreateTokenAsync();
        Task<string> RenewAccessTokenAsync(string refreshToken);
    }
}