using Frontend.Models;
using Frontend.Services.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Frontend.Services
{
    internal class OrdersService : IOrdersService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger _logger;

        private static HttpClient _httpClient = null!;

        public OrdersService(IHttpClientFactory httpClientFactory, ILogger logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _httpClient = _httpClientFactory.CreateClient();
        }

        /// <inheritdoc/>
        /// <exception cref="HttpRequestException"></exception>
        public async Task<IEnumerable<Order>?> GetOrdersAsync()
        {
            IEnumerable<Order>? orders;  

            try
            {
                // Here we use http and not https because we're using a self-signed certificate.
                orders = await _httpClient.GetFromJsonAsync<List<Order>>("http://localhost:5081/api/orders");
                _logger.Information("Orders: {@Orders}", orders);
            }
            catch (Exception e)
            {
                _logger.Error(e, "Failed to get orders.");
                throw;
            }
            
            return orders;
        }
    }
}
