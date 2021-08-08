using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crs.Data
{
    public class CarDb
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CarBrand { get; set; }
        public string Model { get; set; }
        public string RegistrationNumber { get; set; }

        public IList<UserCarDb> UserCars { get; set; }
        
    }
}
