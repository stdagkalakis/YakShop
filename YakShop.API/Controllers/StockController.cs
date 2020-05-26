using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YakShop.API.Data;
using YakShop.Models;

namespace YakShop.API.Controllers
{

    [Route("yak-shop/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly DataContext _context;
        public StockController(DataContext context)
        {
            _context = context;

        }

        // GET yak-shop/stock/5
        [HttpGet("{days}")]
        public async Task<IActionResult> GetStockAfterDays(int days)
        {
            List<Yak> herd = await _context.Yaks.ToListAsync();
            if (herd == null) return BadRequest("Unable to retrieve Yaks from DB.");
            else
            {
                Stock stock = new Stock(herd, days);
                return Ok(stock);
            }
        }
    }
}
