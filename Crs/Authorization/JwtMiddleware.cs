using Crs.Model;
using Crs.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crs.Authorization
{
    public sealed class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private JwtOptions jwtOptions;

        public JwtMiddleware(RequestDelegate next, IOptions<JwtOptions> options)
        {
            _next = next;
            this.jwtOptions = options.Value;
        }

        public async Task Invoke(HttpContext context, IUser userService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                AttachUserToContext(context, userService, token);

            await _next(context);
        }

        private void AttachUserToContext(HttpContext context, IUser userService, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
               
                var key = Encoding.ASCII.GetBytes(jwtOptions.Key);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                   
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var accountId = jwtToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.NameId)?.Value;
                var role = jwtToken.Claims.FirstOrDefault(x => x.Type == "role")?.Value;
               
                var user = userService.GetUser(accountId);
                if (user == null || user.Role != role)
                    return;

                context.Items["User"] = user;
            }
            catch
            {
                // do nothing if jwt validation fails
                // account is not attached to context so request won't have access to secure routes
            }
        }
    }
}
