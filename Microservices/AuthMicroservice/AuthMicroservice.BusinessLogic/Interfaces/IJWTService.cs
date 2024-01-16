﻿using AuthMicroservice.BusinessLogic.Dtos;
using AuthMicroservice.BusinessLogic.Models;

namespace AuthMicroservice.BusinessLogic.Interfaces
{
    public interface IJWTService
    {
        Task<TokenModel> CreateTokenAsync(UserDto userDto);
        Task<string> RenewAccessTokenAsync(string refreshToken);
    }
}