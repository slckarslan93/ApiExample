using ApiExample.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private static List<Order> Orders = new List<Order>();

        [HttpPost]
        public ActionResult<Order> AddOrder(Order order)
        {
            order.Id = Orders.Count > 0 ? Orders.Max(o => o.Id) + 1 : 1;
            Orders.Add(order);
            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetOrders()
        {
            return Orders;
        }

        [HttpGet("{id}")]
        public ActionResult<Order> GetOrderById(int id)
        {
            var order = Orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            return order;
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, Order updatedOrder)
        {
            var order = Orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            order.ProductName = updatedOrder.ProductName;
            order.Quantity = updatedOrder.Quantity;
            order.Price = updatedOrder.Price;
            order.OrderDate = updatedOrder.OrderDate;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var order = Orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            Orders.Remove(order);
            return NoContent();
        }
    }
}

