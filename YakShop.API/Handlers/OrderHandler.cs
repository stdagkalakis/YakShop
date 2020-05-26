using System.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using YakShop.API.Data;
using YakShop.API.Models;
using YakShop.Models;
using System.Linq;

namespace YakShop.API.Handlers
{
    public class OrderHandler
    {
        private readonly DataContext _context;
        public OrderHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<OrderDetail> CheckOrder(OrderDetail orderDetail, int days)
        {

            double requestedMilk = orderDetail.Order.Milk;
            int requestedSkins = orderDetail.Order.Skins;

            // Retrieve herd and orders  from db.
            var herd = await _context.Yaks.ToListAsync();
            var orders = await _context.OrderDetails.Where(x => true).Include(x => x.Order).ToListAsync();

            // Calculate orders.
            var stock = new Stock(herd, days);

            // Remove previous orders from stock.
            foreach (OrderDetail order in orders)
            {
                stock.skinsOfWool -= order.Order.Skins;
                stock.litersOfMik -= Math.Round(order.Order.Milk, 2);
            }

            // If requested order can not be fullfield set to 0 to avoid serialisation, and avoid adding orders that can not be fullfield in Db.
            orderDetail.Order.Milk = (stock.litersOfMik - requestedMilk >= 0) ? requestedMilk : 0;
            orderDetail.Order.Skins = (stock.skinsOfWool - requestedSkins >= 0) ? requestedSkins : 0;

            if (orderDetail.Order.Milk > 0 || orderDetail.Order.Skins > 0)
            {
                bool added = await AddOrder(orderDetail);
                if (added) return orderDetail;
            }

            return null;
        }

        public async Task<bool> AddOrder(OrderDetail orderDetail)
        {
            // Create order entry.
            var order = new OrderDetail()
            {
                Id = Guid.NewGuid(),
                Customer = orderDetail.Customer,
                Order = new Order()
                {
                    Id = Guid.NewGuid(),
                    Milk = orderDetail.Order.Milk,
                    Skins = orderDetail.Order.Skins
                }
            };

            _context.OrderDetails.Add(order);

            var added = await _context.SaveChangesAsync() > 0;

            return added;
        }


    }
}