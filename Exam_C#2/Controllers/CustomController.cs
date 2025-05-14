using Exam_C_2.Context;
using Exam_C_2.Dto;
using Exam_C_2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Exam_C_2.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomController : ControllerBase
    {   
        private readonly ApplicationContextDb _context;
        public CustomController(ApplicationContextDb contextDb) {
            _context = contextDb;
        }
        // GET: api/<CustomController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {   
            var customers = await _context.Customers.ToListAsync();
            return Ok(customers);
        }

        // GET api/<CustomController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute]int id)
        {   
            var customer = await _context.Customers.FindAsync(id);
            return Ok(customer);
        }

        // POST api/<CustomController>
        [HttpPost("register")]
        public async Task<IActionResult> Post([FromBody] CustomerRequestDto value)
        {
            Customer newCustomer = new Customer();
            newCustomer.FullName = value.FullName;
            newCustomer.PhoneNumber = value.PhoneNumber;
            newCustomer.Registration = DateTime.Now;
            await _context.Customers.AddAsync(newCustomer);
            _context.SaveChanges();
            return Ok("Add new customer success.");
        }

        // PUT api/<CustomController>/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> Put([FromRoute]int id, [FromBody] CustomerRequestDto value)
        {
            var customer = await _context.Customers.FindAsync(id);
            customer.FullName = value.FullName;
            customer.PhoneNumber = value.PhoneNumber;
            _context.SaveChanges();
            return Ok("Update customer success.");
        }

        // DELETE api/<CustomController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var customer = _context.Customers.Find(id);
            _context.Customers.Remove(customer);
            _context.SaveChanges();
            return Ok("Delete customer success.");
        }
    }
}
