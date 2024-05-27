using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class OrderDetailsController : ControllerBase
    {
        private readonly StoreDBContext _context;
        private readonly IMapper _mapper;

        public OrderDetailsController(StoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/OrderDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetail>>> GetOrderDetail()
        {
          if (_context.OrderDetail == null)
          {
              return NotFound();
          }
            return await _context.OrderDetail.ToListAsync();
        }

        // GET: api/OrderDetails/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderDetail(int id)
        {
          if (_context.OrderDetail == null)
          {
              return NotFound();
          }
            var orderDetail = _context.OrderDetail
                .Include(x => x.Product)
                .Where(m => m.CustomerOrderID == id);

            if (orderDetail == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<OrderDetailDTO>(orderDetail));
        }

       
        // POST: api/OrderDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderDetail>> PostOrderDetail([FromBody] List<OrderDetailDTO> orderDetail)
        {
            foreach (var item in orderDetail)
            {
                item.CustomerOrder = null;
                item.Product = null;
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var od = _mapper.Map<IEnumerable<OrderDetail>>(orderDetail);
            _context.OrderDetail.AddRange(od);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderDetail", new { id = od.First().CustomerOrderID }, orderDetail);
        }

        // DELETE: api/OrderDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderDetail([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orderDetail = _context.OrderDetail.Where(m => m.CustomerOrderID == id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            _context.OrderDetail.RemoveRange(orderDetail);
            await _context.SaveChangesAsync();

            return Ok(orderDetail);
        }

        private bool OrderDetailExists(int id)
        {
            return (_context.OrderDetail?.Any(e => e.CustomerOrderID == id)).GetValueOrDefault();
        }
    }
}
