using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crs.Data
{
    public class OrderDb
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CreateDate { get; set; }
        public int CarId { get; set; }
        public string CarBrand { get; set; }
        public string CarModel { get; set; }
        public int CustomerId { get; set; }
        public int MechanicId { get; set; }
        public UserDb User { get; set; }
    }
}
