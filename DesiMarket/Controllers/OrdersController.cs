using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DesiMarket.Models;
using DesiMarket.Repositories;
using Microsoft.Extensions.Logging;
using log4net.Repository.Hierarchy;

namespace DesiMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersRepository _orderRepository;
        private readonly IUsersRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly ILogger<OrdersController> _logger;
        public OrdersController(IOrdersRepository orderRepository, IUsersRepository userRepository, IProductRepository productRepository, ILogger<OrdersController> logger)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult GetOrders()
        {
            var orders = _orderRepository.GetOrders();
            return Ok(orders);
        }
        [HttpGet("{id}")]
        public IActionResult GetOrderById(int id)
        {
            var order = _orderRepository.GetOrderById(id);
            if (order == null)
            {
                _logger.LogError("Invalid id");
                return NotFound();
            }
            return Ok(order);
        }
        [HttpGet("user/{userId}")]
        public IActionResult GetOrdersByUserId(int userId)
        {
            var orders = _orderRepository.GetOrdersByUserId(userId);
            _logger.LogInformation("Orders feteched successfully");
            return Ok(orders);
        }
        [HttpPost]
        [Route("CreateOrder")]
        public IActionResult CreateOrder([FromBody] Orders order)
        {
            if (order == null)
            {
                _logger.LogError("Invalid order");
                return BadRequest();
            }
            _logger.LogInformation("Ordered successful");
            _orderRepository.AddOrder(order);
            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
        }
        ////[HttpGet]
        //[Route("CancelOrder/{id}")]
        //public async IActionResult CancelOrder(int id)
        //{
        //    var u = await _orderRepository.GetOrderById(id);
        //    u.status = "Cancelled";
        //    await _orderRepository.UpdateOrder(u);
        //    return Ok( );

        //}
        //[HttpPut("{id}")]
        //public  IActionResult Cancelorder(int id)
        //{
        //    var u =  _orderRepository.GetOrderById(id);
        //    u.Status = "cancelled";
        //    return Ok();
        //}

        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, [FromBody] Orders order)
        {
            if (order == null || id != order.Id)
            {
                return BadRequest();
            }
            var existingOrder = _orderRepository.GetOrderById(id);
            if (existingOrder == null)
            {
                return NotFound();
            }
            existingOrder.UserId = order.UserId;
            existingOrder.ProductId = order.ProductId;
            existingOrder.Quantity = order.Quantity;
            existingOrder.Status = order.Status;
            existingOrder.TotalPrice = order.TotalPrice;
            _orderRepository.UpdateOrder(existingOrder);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var order = _orderRepository.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            _orderRepository.DeleteOrder(order);
            return NoContent();
        }

        //private readonly OnlineGroceryDbContext _context;

        //public OrdersController(OnlineGroceryDbContext context)
        //{
        //    _context = context;
        //}

        //// GET: api/Orders
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Orders>>> GetOrder()
        //{
        //    return await _context.Order.ToListAsync();
        //}

        //// GET: api/Orders/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Orders>> GetOrders(int id)
        //{
        //    var orders = await _context.Order.FindAsync(id);

        //    if (orders == null)
        //    {
        //        return NotFound();
        //    }

        //    return orders;
        //}

        //// PUT: api/Orders/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutOrders(int id, Orders orders)
        //{
        //    if (id != orders.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(orders).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!OrdersExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Orders
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        //public async Task<ActionResult<Orders>> PostOrders(Orders orders)
        //{
        //    _context.Order.Add(orders);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetOrders", new { id = orders.Id }, orders);
        //}

        //// DELETE: api/Orders/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Orders>> DeleteOrders(int id)
        //{
        //    var orders = await _context.Order.FindAsync(id);
        //    if (orders == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Order.Remove(orders);
        //    await _context.SaveChangesAsync();

        //    return orders;
        //}

        //private bool OrdersExists(int id)
        //{
        //    return _context.Order.Any(e => e.Id == id);
        //}
    }
}
