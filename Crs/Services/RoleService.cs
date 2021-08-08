using Crs.Data;
using Crs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Crs.Services
{
    public class RoleService : IRole
    {
        private readonly ICrsContext db;

        public RoleService(ICrsContext db)
        {
            this.db = db;
        }
        public Model.RoleDto Get(int userId)
        {
            var user = db.Users.FirstOrDefault(u => u.Id == userId);
            return new Model.RoleDto()
            {
                Role = user?.Role?.Role
            };
        }
    }
}
