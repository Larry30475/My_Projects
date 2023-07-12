using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projekt.Data;
using Projekt.Models;
using System.Text.RegularExpressions;

namespace Projekt.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeContext _context;

        public EmployeesController(EmployeeContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
        {
            return await _context.EmployeeItems.ToListAsync();
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(long id)
        {
            var employee = await _context.EmployeeItems.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(long id, Employee employee)
        {
            if (id != employee.Id || !IsCorrectEmployee(employee))
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

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
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            if (!IsCorrectEmployee(employee))
            {
                return BadRequest();
            }

            _context.EmployeeItems.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(long id)
        {
            var employee = await _context.EmployeeItems.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.EmployeeItems.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("filter/{position}")]
        public async Task<ActionResult<IEnumerable<Employee>>> FilterByPosition(string position)
        {
            return await _context.EmployeeItems
                .Where(m => m.Position == position)
                .ToListAsync();
        }

        [HttpGet("noemployees")]
        public async Task<ActionResult<int>> GetNoEmployees()
        {
            var employees = await _context.EmployeeItems
                .ToListAsync();

            return employees.Count;
        }

        [HttpGet("authors")]
        public ActionResult<string> GetAuthors()
        {
            return "Wiktor Sadowy 260373, Ivan Luzhanskyi 247372";
        }

        private bool EmployeeExists(long id)
        {
            return (_context.EmployeeItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private bool IsCorrectEmployee(Employee employee)
        {
            return employee.Name.All(c => Char.IsLetter(c) || c == ' ') && employee.Name.Length > 0 &&
                employee.Position.Length <= 20 && employee.Position.Length > 0 &&
                employee.Salary >= 0 && 
                employee.DateHired <= DateTime.Today;
        }
    }
}
