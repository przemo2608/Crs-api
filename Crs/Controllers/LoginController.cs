using Crs.Model;
using Crs.Model.View;
using Crs.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Crs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private IConfiguration _config;
        private IUser _userService;
        private IRole _roleService;
        private IPasswordHasher<UserDto> passwordHasher;
        private JwtOptions jwtOptions;

        public LoginController(IConfiguration config, IUser userService, IRole roleService, IPasswordHasher<UserDto> passwordHash, IOptions<JwtOptions> options)
        {
            _config = config;
            _userService = userService;
            _roleService = roleService;
            passwordHasher = passwordHash;
            this.jwtOptions = options.Value;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserModel login)
        {
            IActionResult response = Unauthorized();
            var user = AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString, user = user });
            }

            return response;
        }

        private string GenerateJSONWebToken(UserDto userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.NameId, userInfo.Username),
                new Claim("role", userInfo.Role)
            };

            var token = new JwtSecurityToken("localhost",
                "localhost",
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserDto AuthenticateUser(UserModel login)
        {
            var user =  _userService.GetUser(login.Username);
            if(user == null)
            {
                return null;
            }

            
            var authenticated = passwordHasher.VerifyHashedPassword(user, user.Password, login.Password);
            if (authenticated == PasswordVerificationResult.Failed)
            {
                return null;
            }

            var userModel = new UserDto()
            {
                UserId = user.UserId,
                Username = user.Username,
                Role = user.Role, 
                
            };   
            
            return userModel;
        }
    }
}
