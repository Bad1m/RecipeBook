using AuthMicroservice.BusinessLogic.Interfaces;
using AuthMicroservice.BusinessLogic.Models;
using AuthMicroservice.DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AuthMicroservice.BusinessLogic.Services
{
    public class JWTService : IJWTService
    {
        private readonly UserManager<User> _userManager;
        private readonly AuthSettings _authSettings;
        private readonly IUserContext _userContext;

        public JWTService(UserManager<User> userManager, AuthSettings authSettings, IUserContext userContext)
        {
            _userManager = userManager;
            _authSettings = authSettings;
            _userContext = userContext;
        }

        public SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_authSettings.Secret);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        public async Task<List<Claim>> GetClaims()
        {
            var _user = _userContext.CurrentUser;

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, _user.UserName)
        };
            var roles = await _userManager.GetRolesAsync(_user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        public JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken
            (
            claims: claims,
            expires: DateTime.UtcNow.Add(_authSettings.ExpiresIn),
            signingCredentials: signingCredentials
            );
            return tokenOptions;
        }
    
        public async Task<(string accessToken, string refreshToken)> CreateTokenAsync()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            await UpdateRefreshTokenIfExpiredAsync();
            var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return (accessToken, _userContext.CurrentUser.RefreshToken);
        }

        public async Task<string> RenewAccessTokenAsync(string refreshToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
            if (user == null)
            {
                return null;
            }

            _userContext.CurrentUser = user;
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            await UpdateRefreshTokenIfExpiredAsync();
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private async Task UpdateRefreshTokenIfExpiredAsync()
        {
            if (string.IsNullOrEmpty(_userContext.CurrentUser.RefreshToken) || _userContext.CurrentUser.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                var newRefreshToken = GenerateRefreshToken();
                _userContext.CurrentUser.RefreshToken = newRefreshToken;
                _userContext.CurrentUser.RefreshTokenExpiryTime = DateTime.UtcNow.Add(_authSettings.RefreshTokenValidityInDays);
                await _userManager.UpdateAsync(_userContext.CurrentUser);
            }
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}