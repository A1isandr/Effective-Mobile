using Avalonia.Controls;
using Frontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend.Services.Interfaces
{
    public interface ISaveToFileService
    {
        /// <summary>
        /// Asynchronously saves orders to file.
        /// </summary>
        /// <param name="orders"></param>
        /// <returns></returns>
        public Task SaveAsync(IEnumerable<Order> orders);
    }
}
