using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Models
{
    public class Order
    {
        public int Id { get; set; }

        public double Weight { get; set; }

        public int DistrictId { get; set; }

        public DateTime DueTime { get; set; }
    }
}
