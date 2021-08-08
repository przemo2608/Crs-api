using Crs.Data;
using Crs.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crs.Services
{
    public interface IRole 
    {
        RoleDto Get(int userId);
    }
}
