using Crs.Authorization;
using Crs.Model;
using Crs.Model.View;
using Crs.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private IPasswordHasher<UserDto> passwordHasher;
        private readonly ILogger<UsersController> _logger;
        private readonly IUser userService;

        public UsersController(ILogger<UsersController> logger, IUser userService, IPasswordHasher<UserDto> passwordHash)
        {
            _logger = logger;
            this.userService = userService;
            passwordHasher = passwordHash;
        }

        [HttpGet]
        public UserModel Get(string name)
        {

            var user = userService.GetUser(name);
            return new UserModel() {
                Username = user.Username,
                Role = user.Role
            };
        }
       
        [HttpGet]
        [Route("getCustomers")]
        public IEnumerable<UserModel> GetCustomers()
        {
            var customers = userService.GetCustomers();

            return customers.Select(x => new UserModel()
            {
                Id = x.UserId,
                Email = x.Email,
                Name = x.Name,
                Surname = x.Surname,
                Username = x.Username,
                Role = x.Role
            });
        }

        [AdminRole]
        [HttpGet]
        [Route("getWorkers")]
        public IEnumerable<UserModel> GetWorkers()
        {
            var workers = userService.GetWorkers();

            return workers.Select(x => new UserModel()
            {
                Id = x.UserId,
                Email = x.Email,
                Name = x.Name,
                Surname = x.Surname,
                Username = x.Username,
                Role = x.Role
            });
        }

        [HttpPost]
        [Route("createUser")]
        public IActionResult CreateUser(CreateUserModel createUserModel)
        {
            if (!ModelState.IsValid)
                throw new Exception("Invalid model");
            var userDto = new UserDto
            {
                Username = createUserModel.Username,
                Role = createUserModel.Role
            };
            var passwordHash = this.passwordHasher.HashPassword(userDto, createUserModel.Password);
            userService.CreateUser(createUserModel.Username, passwordHash, createUserModel.Role, createUserModel.Email, createUserModel.Name, createUserModel.Surname);
            var user = userService.GetUser(createUserModel.Username);
            return new JsonResult(new UserModel()
            {
                Username = user?.Username,
                Role = user?.Role
            }
            );
        }
        [AdminRole]
        [HttpDelete]
        [Route("deleteUser")]
        public IActionResult DeleteUser(DeleteUserModel deleteUserModel)
        {
            if (!ModelState.IsValid)
                throw new Exception("Invalid model");

            userService.DeleteUser(deleteUserModel.UserId);

            return new OkResult();
        }
    }
}
