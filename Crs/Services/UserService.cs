using Crs.Data;
using Crs.Exceptions;
using Crs.Model;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crs.Services
{
    public class UserService : IUser
    {
        private readonly ICrsContext db;
        

        public UserService(ICrsContext db)
        {
           
            this.db = db;
        }

        public void CreateUser(string username, string password, string role, string email, string name, string surname)
        {
            var user = db.Users.FirstOrDefault(u => u.Username == username);
            var roledb = db.Roles.FirstOrDefault(r => r.Role == role);
            if (user != null)
            {
                throw new Exception("Istnieje taki użytkownik");
            }
            if (roledb == null)
            {
                throw new Exception("Rola nie istnieje");
            }
            user = new UserDb
            {
                Email = email,
                Name = name,
                Surname = surname,
                Username = username,
                Password = password,
                Role = roledb
               
            };
            db.Users.Add(user);
            db.SaveChanges();
        }

        public void DeleteUser(int userId)
        {
            var user = db.Users.FirstOrDefault(x => x.Id == userId);
            if (user == null)
                throw new UserNotFoundException();
            db.Users.Remove(user);
            db.SaveChanges();
        }

     

        public UserDto GetUser(string name)
        {
            var user = db.Users.FirstOrDefault(u => u.Username == name);
            if (user == null) 
                throw new UserNotFoundException();
            var role = db.Roles.FirstOrDefault(r => r.Id == user.RoleId);
           
           
            return new UserDto()
            {
                UserId = user.Id,
                Username = user?.Username,
                Role = role?.Role,
                Password = user?.Password

            };
        }

        public IEnumerable<UserDto> GetUsers()
        {
            var users = db.Users.ToList();            
            var roles = db.Roles.ToDictionary(k => k.Id);

            return users.Select(x => new UserDto()
            {
                Email = x.Email,
                Name = x.Name,
                Surname = x.Surname,
                UserId = x.Id,
                Username = x.Username,
                Role = roles.ContainsKey(x.RoleId) ? roles[x.RoleId]?.Role : throw new Exception()
            });
        }

        public IEnumerable<UserDto> GetWorkers()
        {
            var workerRole = db.Roles.FirstOrDefault(role => role.Role == "Mechanic");
            var workers = db.Users.Where(u => u.RoleId == workerRole.Id).ToList();

            return workers.Select(x => new UserDto()
            {
                UserId = x.Id,
                Email = x.Email,
                Surname = x.Surname,
                Name = x.Name,
                Username = x.Username,
                Role = workerRole.Role
            }
            );
        }

        public IEnumerable<UserDto> GetCustomers()
        {
            var customerRole = db.Roles.FirstOrDefault(role => role.Role == "Customer");
            var customers = db.Users.Where(u => u.RoleId == customerRole.Id).ToList();

            return customers.Select(x => new UserDto()
            {
                UserId = x.Id,
                Email = x.Email,
                Surname = x.Surname,
                Name = x.Name,
                Username = x.Username,
                Role = customerRole.Role
            }
            );
        }
    }
}
