using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using WebApplication.Dtos;
using WebApplication.Extensions;
using WebApplication.Models;
using WebApplication.Repository;

namespace WebApplication.Services
{
    public class UserService
    {
        private readonly UserRepository _repository;

        public UserService(UserRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResponseDto<User>> CreateUser(CreateUserDto dto)
        {
            var validRes = dto.Validate();
            if (!string.IsNullOrEmpty(validRes))
            {
                return new ResponseDto<User>
                {
                    IsSuccess = false,
                    Error = validRes
                };
            }

            var existing = await _repository.ByLogin(dto.Login);
            if (existing is not null)
            {
                return new ResponseDto<User>
                {
                    IsSuccess = false,
                    Error = "Пользователь уже существует"
                };
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Login = dto.Login,
                Password = dto.Password,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                MiddleName = dto.MiddleName
            };

            var identity = GetIdentity(user);
            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            user.Token = encodedJwt;
            
            return new ResponseDto<User>
            {
                IsSuccess = true,
                Content = await _repository.CreateUser(user)
            };
        }

        public async Task<ResponseDto<User>> GetUser(string login, string password)
        {
            var user = await _repository.ByCredentials(login, password);
            if (user is null)
            {
                return new ResponseDto<User>
                {
                    IsSuccess = false,
                    Error = "Пользователь не найден"
                };
            }
            
            return new ResponseDto<User>
            {
                IsSuccess = true,
                Content = user
            };
        }

        public async Task<ResponseDto<User>> UpdatePassword(UpdatePasswordDto dto)
        {
            var validRes = dto.Validate();
            if (!string.IsNullOrEmpty(validRes))
            {
                return new ResponseDto<User>
                {
                    IsSuccess = false,
                    Error = validRes
                };
            }
            
            var user = await _repository.ById(dto.UserId);
            if (user is null)
            {
                return new ResponseDto<User>
                {
                    IsSuccess = false,
                    Error = "Пользователя не существует"
                };
            }
            return new ResponseDto<User>
            {
                IsSuccess = true,
                Content = await _repository.UpdatePassword(dto.UserId, dto.NewPassword)
            };
        }

        public async Task<ResponseDto> Delete(Guid userId)
        {
            await _repository.DeleteUser(userId);
            return new ResponseDto()
            {
                IsSuccess = true
            };
        }
        
        private ClaimsIdentity GetIdentity(User user)
        {
            var claims = new List<Claim>
            {
                new (ClaimsIdentity.DefaultNameClaimType, user.Login),
            };
            var claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
            
            return claimsIdentity;
        }
    }
}