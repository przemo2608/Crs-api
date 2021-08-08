using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Crs.Data
{
    public class RoleDb
    {
       
        public int  Id { get; set; }
        public string Role { get; set; }

        public ICollection<UserDb> Users { get; set; }
    }
}
