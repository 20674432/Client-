using ClientPortal.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ClientPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class OrderController : ControllerBase
    {
        private readonly ClientPortalContext _context;

        public OrderController(ClientPortalContext context)
        {
            _context = context;
        }

        [HttpGet("client/{clientId}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByClientId(int clientId)
        {
           
            var orders = await _context.Orders
                .Where(o => o.ClientId == clientId)
                .Include(o => o.Product)  
                .Include(o => o.Status)   
                .ToListAsync();

            if (orders == null || !orders.Any())
            {
                return NotFound(new { message = "No orders found for this client." });
            }

            return Ok(orders);
        }


        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] OrderDto orderDto)
        {
            
            var client = await _context.Clients.FindAsync(orderDto.ClientId);
            if (client == null)
            {
                return BadRequest(new { message = "Client not found." });
            }

           
            var product = await _context.Products.FindAsync(orderDto.ProductId);
            if (product == null)
            {
                return BadRequest(new { message = "Product not found." });
            }

            
            var order = new Order
            {
                ClientId = orderDto.ClientId,
                ProductId = orderDto.ProductId,
                Quantity = orderDto.Quantity,
                TotalPrice = orderDto.Quantity * product.Price, 
                StatusId = orderDto.StatusId,
                OrderDate = DateTime.UtcNow,
                LastUpdated = DateTime.UtcNow
            };

            // Add the new order to the database
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrdersByClientId), new { clientId = order.ClientId }, order);
        }
       
    }
}
