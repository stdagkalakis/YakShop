using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using YakShop.API.Data;
using YakShop.API.Models;
using YakShop.Models;

namespace YakShop.API.Controllers
{

    [Route("yak-shop/[controller]")]
    [ApiController]
    public class HerdController : ControllerBase
    {
        private readonly DataContext _context;
        public HerdController(DataContext context)
        {
            _context = context;

        }

        // GET yak-shop/herd/T
        //Returns a view of your herd after T days
        [HttpGet("{days}")]
        public async Task<IActionResult> GetHerdViewAfterDays(int days)
        {
            List<Yak> herd = await _context.Yaks.ToListAsync();
            if (herd == null) return BadRequest("Unable to retrieve Yaks from DB.");
            else
            {
                List<YakModel> ret = new List<YakModel>();
                foreach (Yak yak in herd)
                {
                    yak.IncreaseAgeByDays(days);
                    ret.Add(new YakModel() { Name = yak.Name, Age = yak.Age, LastShavedAge = yak.LastShavedAge });
                }

                return Ok(JsonConvert.SerializeObject(new { herd = ret }, Formatting.Indented));
            }
        }

        // GET yak-shop/herd
        //Returns a view of your herd
        [HttpGet]
        public async Task<IActionResult> GetHerdView()
        {
            List<Yak> herd = await _context.Yaks.ToListAsync();
            if (herd == null) return BadRequest("Unable to retrieve Yaks from DB.");
            else
            {
                List<YakModel> ret = new List<YakModel>();
                foreach (Yak yak in herd)
                {
                    ret.Add(new YakModel() { Name = yak.Name, Age = yak.Age, LastShavedAge = yak.LastShavedAge });
                }

                return Ok(JsonConvert.SerializeObject(new { herd = ret }, Formatting.Indented));
            }
        }



        // POST yak-shop/
        // Add herd to db TODO. 
        [HttpPost]
        public async Task<IActionResult> AddYakToHerd(Yak yak)
        {
            System.Console.WriteLine(JsonConvert.SerializeObject(yak, Formatting.Indented));
            var maxId = _context.Yaks.Max(x => x.Id);
            // Yak ids are integers, thus getting the max will be generate a unique id. 
            yak.Id = maxId + 1;

            // Add to db using context.
            _context.Yaks.Add(yak);

            var added = await _context.SaveChangesAsync() > 0;

            return (added) ? StatusCode(201) : StatusCode(204);


        }
    }
}
