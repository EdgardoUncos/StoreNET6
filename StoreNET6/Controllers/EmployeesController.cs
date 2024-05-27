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
    public class EmployeesController : ControllerBase
    {
        private readonly StoreDBContext _context;
        private readonly IMapper _mapper;

        public EmployeesController(StoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployees()
        {
            if (_context.Employee == null)
            {
                return NotFound();
            }
            var lista = _mapper.Map<List<EmployeeDTO>>(await _context.Employee.ToListAsync());
            return lista.ToList();
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployee(int id)
        {
            if (_context.Employee == null)
            {
                return NotFound();
            }
            var employee = await _context.Employee.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<EmployeeDTO>(employee));
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, EmployeeDTO employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            _context.Entry(_mapper.Map<Employee>(employee)).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(EmployeeDTO employee)
        {
            if (_context.Employee == null)
            {
                return Problem("Entity set 'StoreDBContext.Employees'  is null.");
            }
            var e = _mapper.Map<Employee>(employee);
            _context.Employee.Add(e);
            await _context.SaveChangesAsync();
            employee.Id = e.ID;

            return CreatedAtAction("GetEmployee", new { id = e.ID }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (_context.Employee == null)
            {
                return NotFound();
            }
            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<EmployeeDTO>(employee));
        }

        private bool EmployeeExists(int id)
        {
            return (_context.Employee?.Any(e => e.ID == id)).GetValueOrDefault();
        }

    }
}
