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
    public class CustomerOrdersController : ControllerBase
    {
        private readonly StoreDBContext _context;
        private readonly IMapper _mapper;

        public CustomerOrdersController(StoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/CustomerOrders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerOrderDTO>>> GetCustomerOrders()
        {
            if (_context.CustomerOrder == null)
            {
                return NotFound();
            }
            //return await _context.CustomerOrders.ToListAsync();
            var DTO = _mapper.Map<IEnumerable<CustomerOrderDTO>>(await _context.CustomerOrder.ToListAsync());
            return Ok(DTO.ToList());
        }

        // GET: api/CustomerOrders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerOrderDTO>> GetCustomerOrder(int id)
        {
            if (_context.CustomerOrder == null)
            {
                return NotFound();
            }
            var customerOrder = await _context.CustomerOrder.FindAsync(id);

            if (customerOrder == null)
            {
                return NotFound();
            }

            //return customerOrder;

            return _mapper.Map<CustomerOrderDTO>(customerOrder);
        }

        // PUT: api/CustomerOrders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerOrder(int id, CustomerOrderDTO customerOrder)
        {
            if (id != customerOrder.Id)
            {
                return BadRequest();
            }

            customerOrder.OrderDetail = null;
            customerOrder.OrderStatus = null;
            customerOrder.Customer = null;

            _context.Entry(_mapper.Map<CustomerOrder>(customerOrder)).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerOrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CustomerOrders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostCustomerOrder([FromBody] CustomerOrderDTO customerOrder)
        {
            customerOrder.OrderStatus = null;
            customerOrder.Customer = null;
            customerOrder.Date = DateTime.Now;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var co = _mapper.Map<CustomerOrder>(customerOrder);
                _context.CustomerOrder.Add(co);
                await _context.SaveChangesAsync();
                customerOrder.Id = co.ID;

                return CreatedAtAction("GetCustomerOrder", new { id = co.ID }, customerOrder);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        // DELETE: api/CustomerOrders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerOrder(int id)
        {
            if (_context.CustomerOrder == null)
            {
                return NotFound();
            }
            var customerOrder = await _context.CustomerOrder.FindAsync(id);
            if (customerOrder == null)
            {
                return NotFound();
            }

            _context.CustomerOrder.Remove(customerOrder);
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<CustomerOrderDTO>(customerOrder));
        }

        private bool CustomerOrderExists(int id)
        {
            return (_context.CustomerOrder?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
