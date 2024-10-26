using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Frontend.Models;
using Frontend.Services.Interfaces;

namespace Frontend.Test
{
    public class SaveToTxtFileService : ISaveToFileService
    {
        public Task SaveAsync(IEnumerable<Order> orders)
        {
            return Task.Delay(TimeSpan.FromMilliseconds(3000));
        }
    }
}
