using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crs.Model
{
    public class CarDto
    {
        public int CarId { get; set; }
        public int UserId { get; set; }
        public string CarBrand { get; set; }
        public string Model { get; set; }
        public string RegistrationNumber { get; set; }
    }
}
