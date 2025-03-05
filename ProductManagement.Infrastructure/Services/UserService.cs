using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProductManagement.Application.Contracts.Interfaces;
using ProductManagement.Application.DTOs;
using ProductManagement.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        public UserService(UserManager<ApplicationUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<string> RegisterAsync(string email, string password, string fullName)
        {

            var user = new ApplicationUser
            {
                Email = email,
                UserName = email,
                FullName = fullName
            };
            var result =await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
                    throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

                await _userManager.AddToRoleAsync(user, "User");
            return user.Id;
           
        }
        public async Task<string> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, password))
            {
                throw new Exception("Invalid data,User ot found");
            }
            var roles = await _userManager.GetRolesAsync(user);
            var tokenPayload = new TokenPayloadDTO { UserId = user.Id, Email = user.Email, Roles = roles };
            return await _tokenService.GenerateToken(tokenPayload);
        }

       
    }
}
