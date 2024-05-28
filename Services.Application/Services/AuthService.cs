using Domain.Models;
using Domain.View;
using Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Core.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private string secretKey;
        public AuthService(ApplicationDbContext dbContext, IConfiguration configuration,
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<ApiResponse> LoginAsync(LoginRequestView model)
        {
            var response = new ApiResponse();
            ApplicationUser userFromDb = _dbContext.ApplicationUsers
                    .FirstOrDefault(u => u.UserName.ToLower() == model.UserName.ToLower());

            bool isValid = await _userManager.CheckPasswordAsync(userFromDb, model.Password);
            if (!isValid)
            {
                response.Result = new LoginResponseView();
                response.IsSuccess = false;
                response.ErrorMessages.Add("Username or password is incorrect");
                return response;
            }

            var roles = await _userManager.GetRolesAsync(userFromDb);
            JwtSecurityTokenHandler tokenHandler = new();
            await Console.Out.WriteLineAsync("secretkey");
            await Console.Out.WriteLineAsync(secretKey);
            byte[] key = Encoding.ASCII.GetBytes(secretKey);

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("fullName", userFromDb.Name),
                    new Claim("id", userFromDb.Id.ToString()),
                    new Claim(ClaimTypes.Email, userFromDb.UserName.ToString()),
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault()),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            LoginResponseView loginResponse = new()
            {
                Email = userFromDb.Email,
                Token = tokenHandler.WriteToken(token)
            };
            await Console.Out.WriteLineAsync(loginResponse.Email);
            await Console.Out.WriteLineAsync(loginResponse.Token);

            if (loginResponse.Email == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                response.IsSuccess = false;
                response.ErrorMessages.Add("Username or password is incorrect");
                return response;
            }

            response.IsSuccess = true;
            response.Result = loginResponse;
            return response;
        }

        public async Task<ApiResponse> Register(RegisterRequestView model)
        {
            var response = new ApiResponse();

            ApplicationUser userFromDb = _dbContext.ApplicationUsers
                    .FirstOrDefault(u => u.UserName.ToLower() == model.UserName.ToLower());
            if (userFromDb != null)
            {
                response.IsSuccess = false;
                response.ErrorMessages.Add("Username already exists");
                return response;
            }

            ApplicationUser newUser = new()
            {
                UserName = model.UserName,
                Email = model.UserName,
                NormalizedEmail = model.UserName.ToUpper(),
                Name = model.Name
            };

            try
            {
                var result = await _userManager.CreateAsync(newUser, model.Password);
                if (result.Succeeded)
                {
                    if (!_roleManager.RoleExistsAsync("admin").GetAwaiter().GetResult())
                    {
                        await _roleManager.CreateAsync(new IdentityRole("admin"));
                    }
                    await _userManager.AddToRoleAsync(newUser, "admin");

                    response.IsSuccess = true;
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages.Add(ex.ToString());
                return response;
            }
            response.IsSuccess = false;
            response.ErrorMessages.Add("Error while registering");
            return response;
        }
    }
}
