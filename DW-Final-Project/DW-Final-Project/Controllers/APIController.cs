using DW_Final_Project.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
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
            pwd = EncriptarSenha(pwd);
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

        //GET: Get All Products
        [HttpGet("/API/Products")]
        public async Task<IActionResult> Products()
        {
            var products = await _context.Product.ToListAsync();

            if (products == null)
            {
                return Ok();
            }

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            var serializedUser = JsonSerializer.Serialize(products, options);

            return Ok(serializedUser);
        }

        //GET: Get All Products
        [HttpGet("/API/Profile/{email}")]
        public async Task<IActionResult> Profile(string email)
        {
            if (email == null)
            {
                return BadRequest();
            }
            var person = await _context.Person
                .Include(u => u.user)
                .FirstOrDefaultAsync(m => m.user.email==email);

            if (person == null)
            {
                return NotFound();
            }

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            var serializedUser = JsonSerializer.Serialize(person, options);

            return Ok(serializedUser);
        }

        private string EncriptarSenha(string senha)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(senha);
                byte[] hash = sha256.ComputeHash(bytes);

                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < hash.Length; i++)
                {
                    builder.Append(hash[i].ToString("x2")); // Converte o byte em uma string hexadecimal
                }

                return builder.ToString();
            }
        }


    }
}
