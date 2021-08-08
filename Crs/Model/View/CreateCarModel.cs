using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crs.Model.View
{
    public class CreateCarModel
    {
        public int userId { get; set; }
        public string CarBrand { get; set; }
        public string Model { get; set; }
        public string RegistrationNumber { get; set; }
    }
}
