using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using YakShop.API.Data;
using YakShop.API.Handlers;
using YakShop.API.Models;
using YakShop.Models;

namespace YakShop.API.Controllers
{
    [Route("yak-shop/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly DataContext _context;
        private OrderHandler handler;
        public OrderController(DataContext context)
        {
            _context = context;
            handler = new OrderHandler(context);
        }

        // POST yak-shop/order
        [HttpPost("{days}")]
        public async Task<IActionResult> PostOrder(int days, OrderDetail sm)
        {
            var requestMilk = sm.Order.Milk;
            var requestSkins = sm.Order.Skins;
            if (days <= 0 || sm.Order.Skins < 0 || sm.Order.Milk < 0) return StatusCode(400, "Days and order must be positive numbers.");

            var checkedOrder = await handler.CheckOrder(sm, days);

            // Null means order was not fullfield. 
            if (checkedOrder == null) return StatusCode(404);

            // Skins or Milk == 0 means this part of the order couln't be fullfield
            if (checkedOrder.Order.Skins != requestSkins || checkedOrder.Order.Milk != requestMilk)
            {
                return StatusCode(206, JsonConvert.SerializeObject(checkedOrder, Formatting.Indented));
            }

            // Otherwise everything went well and we managed to add the order.
            return StatusCode(201, JsonConvert.SerializeObject(checkedOrder, Formatting.Indented));

        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _context.OrderDetails.Where(x => true).Include(x => x.Order).ToListAsync();
            return (orders != null) ? StatusCode(200, JsonConvert.SerializeObject(orders)) : StatusCode(204, "Unable to retrieve orders");
        }

    }

}