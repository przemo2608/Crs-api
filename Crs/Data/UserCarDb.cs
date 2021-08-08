using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crs.Data
{
    public class UserCarDb
    {
        public int UserId { get; set; }
        public UserDb User { get; set; }

        public int CarId { get; set; }
        public CarDb Car { get; set; }
    }
}
