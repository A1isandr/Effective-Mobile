using EffectiveMobile.Backend.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EffectiveMobile.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            using ApplicationContext db = new();

            var orders = db.Orders.ToList();

            return Ok(orders);
        }   
    }
}
