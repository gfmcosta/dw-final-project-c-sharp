using DW_Final_Project.Data;
using DW_Final_Project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;

namespace DW_Final_Project.Controllers
{
    public class APIController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public APIController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _signInManager = signInManager;
            _context = context;
            _userManager = userManager;
        }

        // GET: User Login
        [HttpGet("/API/Login/{email}/{pwd}")]
        public async Task<IActionResult> Login(string email, string pwd)
        {
            string decodedPassword = HttpUtility.UrlDecode(pwd);
            if (email != null && pwd != null)
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user.Email, decodedPassword, false, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        var person = await _context.Person.FirstOrDefaultAsync(p => p.userId == user.Id);
                        if (person != null)
                        {
                            var userWithPerson = new
                            {
                                email= email,
                                person = person
                            };
                            return Ok(userWithPerson);
                        }
                    }
                }

                return NotFound();
            }

            return BadRequest();
        }

        //GET: Get All Products
        [HttpGet("/API/Products")]
        public async Task<IActionResult> Products()
        {
            var products = await _context.Product.ToListAsync();

            if (products == null)
            {
                return NotFound();
            }

            return Ok(products);
        }

        //GET: Get All Categories
        [HttpGet("/API/Category")]
        public async Task<IActionResult> Category()
        {
            var categories = await _context.Category.ToListAsync();

            if (categories == null)
            {
                return NotFound();
            }

            return Ok(categories);
        }

        //GET: Get All Orders
        [HttpGet("/API/Order")]
        public async Task<IActionResult> Order()
        {
            var orders = await _context.Order.ToListAsync();

            if (orders == null)
            {
                return NotFound();
            }

            return Ok(orders);
        }

        //GET: Get All Order Items
        [HttpGet("/API/OrderItems")]
        public async Task<IActionResult> OrderItem()
        {
            var orderItems = await _context.OrderItem.ToListAsync();

            if (orderItems == null)
            {
                return NotFound();
            }

            return Ok(orderItems);
        }

        //GET: Get All Person
        [HttpGet("/API/Person")]
        public async Task<IActionResult> Person()
        {
            var p = await _context.Person.ToListAsync();

            if (p == null)
            {
                return NotFound();
            }

            return Ok(p);
        }


        //GET: Get All Product Season
        [HttpGet("/API/ProductSeason")]
        public async Task<IActionResult> ProductSeason()
        {
            var ps = await _context.Product_Season.ToListAsync();

            if (ps == null)
            {
                return NotFound();
            }

            return Ok(ps);
        }

        //GET: Get Product by id
        [HttpGet("/API/Products/{id}")]
        public async Task<IActionResult> getProductById(int id)
        {
            var product = await _context.Product.FindAsync(id);

            if (product == null)
            {
                return Ok();
            }

            return Ok(product);
        }

        //GET: Get Profile
        [HttpGet("/API/Profile/{id}")]
        public async Task<IActionResult> Profile(int id)
        {
            if (id == null || id==0)
            {
                return BadRequest();
            }
            var person = await _context.Person.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        //POST: Profile Updates
        [HttpPost("/API/updateProfile/{id}")]
        public IActionResult UpdatePerson(int id, [FromBody] Person personUpdateModel)
        {
            // Lógica para atualizar os dados da pessoa com o email fornecido

            // Exemplo de implementação básica para atualizar os dados da pessoa
            var person = _context.Person.FirstOrDefault(p => p.id == id);

            if (person != null)
            {
                person.name = personUpdateModel.name;
                person.phoneNumber = personUpdateModel.phoneNumber;
                person.address = personUpdateModel.address;
                person.postalCode = personUpdateModel.postalCode;
                person.dataNasc = personUpdateModel.dataNasc;
                person.gender = personUpdateModel.gender;

                if (personUpdateModel.imagePath.StartsWith("data:image/"))
                {
                    int startIndex = personUpdateModel.imagePath.IndexOf("/") + 1; // Encontra a posição do primeiro caractere após "/"
                    int endIndex = personUpdateModel.imagePath.IndexOf(";"); // Encontra a posição do último caractere antes de ";"
                    string extFoto = personUpdateModel.imagePath.Substring(startIndex, endIndex - startIndex);

                    // Remova o prefixo "data:image/jpeg;base64," da string
                    string base64String = personUpdateModel.imagePath.Substring(personUpdateModel.imagePath.IndexOf(',') + 1);

                    // Decodifique a string base64 em um array de bytes
                    byte[] imagemBytes = Convert.FromBase64String(base64String);
                    //converter byte[] para ficheiro
                    string nomeArquivo = Guid.NewGuid().ToString() + "_" + person.id + "." + extFoto;
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", nomeArquivo);

                    using (FileStream fs = new FileStream(filePath, FileMode.Create))
                    {
                        fs.Write(imagemBytes, 0, imagemBytes.Length);
                    }
                    person.imagePath = nomeArquivo;
                }
                else
                {
                    //mesma imagem
                    person.imagePath = personUpdateModel.imagePath;
                }

                _context.Update(person);
                _context.SaveChanges();

                return Ok("Dados da pessoa atualizados com sucesso.");
            }

            return NotFound("Pessoa não encontrada.");
        }


        // POST: Create OrderItems
        [HttpPost("/API/orders/{id}")]
        public IActionResult CreateOrderItems(int id, [FromBody] List<OrderItem> orderItemList)
        {
            var order = new Order
            {
                price = 0,
                IVA = 23,
                personFK = id
            };

            _context.Order.Add(order);
            _context.SaveChanges();

            foreach (var orderItemModel in orderItemList)
            {
                var orderItem = new OrderItem
                {
                    quantity = orderItemModel.quantity,
                    totalPrice = orderItemModel.totalPrice,
                    orderFK = order.id,
                    productFK = orderItemModel.productFK,
                    size = orderItemModel.size
                };

                _context.OrderItem.Add(orderItem);
                _context.SaveChanges();
                order.price += orderItem.totalPrice;
                _context.Order.Update(order);
                _context.SaveChanges();
            }

            return Ok("Order items created successfully.");
        }



    }
}
