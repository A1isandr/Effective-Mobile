using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicData;
using Frontend.Models;

namespace Frontend.Services.Interfaces
{
    public interface IOrdersService
    {
        /// <summary>
        /// Get all available orders.
        /// </summary>
        /// <returns>All orders.</returns>
        public Task<IEnumerable<Order>?> GetOrdersAsync();
    }
}
