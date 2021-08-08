using Crs.Authorization;
using Crs.Data;
using Crs.Exceptions;
using Crs.Model;
using Crs.Model.View;
using Crs.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Crs
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddDefaultPolicy(
                builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                    
                }
                 ));
            services.AddControllers(options =>
            {
                options.Filters.Add(new HttpResponseExceptionFilter());
            });
           
            services.Configure<JwtOptions>(options => Configuration.GetSection("Jwt").Bind(options));
            
          
            services.AddRazorPages();

           services.AddDbContext<CrsContext> (options => {
               var cs = Configuration.GetConnectionString("CrsDb");
               options.UseSqlServer(cs);
               });
            services.AddScoped<ICrsContext>(provider => (CrsContext)provider.GetService(typeof(CrsContext)));
            services.AddScoped<IUser, UserService>();
            services.AddScoped<IRole, RoleService>();
            services.AddScoped<INews, NewsService>();
            services.AddScoped<IOrder, OrderService>();
            services.AddScoped<ICar, CarService>();
            services.AddScoped<IPasswordHasher<UserDto>, PasswordHasher<UserDto>>();
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
          
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();
           

            app.UseAuthorization();
            app.UseAuthentication();
            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
