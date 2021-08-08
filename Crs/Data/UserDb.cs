using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Crs.Data
{
    public class UserDb
    {

        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public int RoleId { get; set; }
        public RoleDb Role { get; set; }

        public IList<UserCarDb> UserCars { get; set; }

        public ICollection<OrderDb> Orders { get; set; }
    }
}
