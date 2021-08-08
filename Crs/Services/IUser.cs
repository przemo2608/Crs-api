using Crs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crs.Services
{
    public interface IUser
    {
        UserDto GetUser(string name);

        IEnumerable<UserDto> GetUsers();
        IEnumerable<UserDto> GetCustomers();
        IEnumerable<UserDto> GetWorkers();

        void CreateUser(string username, string password, string role, string email, string name, string surname);
        void DeleteUser(int userId);
    }
}
