using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frontend.Models;
using Frontend.Services.Interfaces;

namespace Frontend.Test
{
    public class OrdersService : IOrdersService
    {
        public Task<IEnumerable<Order>?> GetOrdersAsync()
        {
            return new Task<IEnumerable<Order>?>(() => new List<Order>
            {
                new Order
                {
                    Id = 1,
                    Weight = 100,
                    DistrictId = 1,
                    DueTime = DateTime.Now
                }
            });
        }
    }
}
