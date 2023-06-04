using DW_Final_Project.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DW_Final_Project.Controllers
{
    public class APIController : Controller
    {
        private readonly ApplicationDbContext _context;

        public APIController(ApplicationDbContext context)
        {
            _context = context;
        }

        //GET: User Login
        [HttpGet("/API/Login/{email}/{pwd}")]
        public async Task<IActionResult> Login(string email, string pwd)
        {
            if (email == null || pwd == null || email == "" || pwd == "" || _context.User == null)
            {
                return BadRequest();
            }

            var user = await _context.User
                .Include(u => u.type)
                .FirstOrDefaultAsync(m => m.email.Equals(email) && m.password.Equals(pwd));

            if (user == null)
            {
                return NotFound();
            }

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            var serializedUser = JsonSerializer.Serialize(user, options);

            return Ok(serializedUser);
        }

        //POST: User Register
        [HttpPost("/API/Register")]
        public async Task<IActionResult> Register(string email, string pwd)
        {
            if (email == null || pwd == null || email == "" || pwd == "" || _context.User == null)
            {
                return BadRequest();
            }

            var user = await _context.User
                .Include(u => u.type)
                .FirstOrDefaultAsync(m => m.email.Equals(email) && m.password.Equals(pwd));

            if (user == null)
            {
                return NotFound();
            }

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            var serializedUser = JsonSerializer.Serialize(user, options);

            return Ok(serializedUser);
        }
    }
}
