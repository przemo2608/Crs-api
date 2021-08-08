using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crs.Model.View
{
    public class CreateOrderModel
    {
        public int OrderId { get; set; }
        public string Status { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CreateDate { get; set; }
        public int CarId { get; set; }
        public int CustomerId { get; set; }
        public int MechanicId { get; set; }
    }
}
