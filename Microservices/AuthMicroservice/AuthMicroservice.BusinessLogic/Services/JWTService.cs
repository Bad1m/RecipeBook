using AuthMicroservice.BusinessLogic.Constants;
using AuthMicroservice.BusinessLogic.Dtos;
using AuthMicroservice.BusinessLogic.Interfaces;
using AuthMicroservice.BusinessLogic.Models;
using AuthMicroservice.DataAccess.Entities;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public JWTService(UserManager<User> userManager, AuthSettings authSettings, IMapper mapper)
        {
            _userManager = userManager;
            _authSettings = authSettings;
            _mapper = mapper;
        }

        public async Task<TokenModel> CreateTokenAsync(UserDto userDto)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaimsAsync(userDto);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            await UpdateRefreshTokenIfExpiredAsync(userDto);
            var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return new TokenModel
            {
                AccessToken = accessToken,
                RefreshToken = userDto.RefreshToken
            };
        }

        public async Task<TokenModel> RenewAccessTokenAsync(string refreshToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(user => user.RefreshToken == refreshToken);

            if (user == null)
            {
                throw new InvalidOperationException(ErrorMessages.UserNotFoundForRefreshToken);
            }

            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaimsAsync(_mapper.Map<UserDto>(user));
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            await UpdateRefreshTokenIfExpiredAsync(_mapper.Map<UserDto>(user));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return new TokenModel
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_authSettings.Secret);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaimsAsync(UserDto userDto)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, userDto.UserName)
        };
            var roles = await _userManager.GetRolesAsync(_mapper.Map<User>(userDto));

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken
            (
            claims: claims,
            expires: DateTime.UtcNow.Add(_authSettings.ExpiresIn),
            signingCredentials: signingCredentials
            );

            return tokenOptions;
        }
    
        private async Task UpdateRefreshTokenIfExpiredAsync(UserDto userDto)
        {
            if (string.IsNullOrEmpty(userDto.RefreshToken) || userDto.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                var newRefreshToken = GenerateRefreshToken();
                userDto.RefreshToken = newRefreshToken;
                userDto.RefreshTokenExpiryTime = DateTime.UtcNow.Add(_authSettings.RefreshTokenValidityInDays);
                var user = await _userManager.FindByNameAsync(userDto.UserName);
                user.RefreshToken = userDto.RefreshToken;
                user.RefreshTokenExpiryTime = userDto.RefreshTokenExpiryTime;
                await _userManager.UpdateAsync(user);
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