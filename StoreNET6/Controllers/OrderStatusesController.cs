using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreNET6.Data;
using StoreNET6.DTOs;
using StoreNET6.Models;

namespace StoreNET6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderStatusesController : ControllerBase
    {
        private readonly StoreDBContext _context;
        private readonly IMapper _mapper;

        public OrderStatusesController(StoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/OrderStatuses
        [HttpGet]
        public IEnumerable<OrderStatusDTO> GetOrderStatus()
        {
            return _mapper.Map<IEnumerable<OrderStatusDTO>>(_context.OrderStatus.OrderBy(x => x.Name));
        }

        // GET: api/OrderStatuses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderStatus([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orderStatus = await _context.OrderStatus.SingleOrDefaultAsync(m => m.ID == id);

            if (orderStatus == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<OrderStatusDTO>(orderStatus));
        }

        // POST: api/OrderStatuses
        [HttpPost]
        public async Task<IActionResult> PostOrderStatus([FromBody] OrderStatusDTO orderStatus)
        {
            orderStatus.CustomerOrder = null;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var os = _mapper.Map<OrderStatus>(orderStatus);

            _context.OrderStatus.Add(os);
            await _context.SaveChangesAsync();
            orderStatus.Id = os.ID;

            return CreatedAtAction("GetOrderStatus", new { id = os.ID }, orderStatus);
        }
    }
}
