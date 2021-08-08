using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crs.Model.View
{
    public class RoleModel
    {
        public int RoleId { get; set; }
        public string Role { get; set; }

        public int UserId { get; set; }
        public UserModel UserModel { get; set; }
    }
}
