﻿using DW_Final_Project.Areas.Identity.Pages.Account;
using DW_Final_Project.Data;
using DW_Final_Project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.Globalization;
using System.Net.WebSockets;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;

namespace DW_Final_Project.Controllers
{
    public class APIController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _context;

        public APIController(UserManager<IdentityUser> userManager,
            IUserStore<IdentityUser> userStore,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
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

        //GET: Get All Products
        [HttpGet("/API/OrderHistory/{id}")]
        public async Task<IActionResult> OrderHistory(int id)
        {
            var orderHistory = await _context.Order.Where(o=> o.personFK ==id).ToListAsync();

            if (orderHistory == null)
            {
                return NotFound();
            }

            return Ok(orderHistory);
        }

        //GET: Get All Products
        [HttpGet("/API/orderItems/{id}")]
        public async Task<IActionResult> OrderItemsByOrderId(int id)
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve, // Preserve object references
                MaxDepth = 32 // Set the maximum depth
            };

            var orderItems = await _context.OrderItem
                .Where(o => o.orderFK == id)
                .Include(o => o.product)
                .ToListAsync();

            if (orderItems == null)
            {
                return NotFound();
            }

            var json = JsonSerializer.Serialize(orderItems, options);

            return Ok(json);
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


        // POST: Create Category
        [HttpPost("/API/category/create")]
        public IActionResult CreateCategory([FromBody] Category category)
        {
            try
            {
            _context.Category.Add(category);
            _context.SaveChanges();
            return Ok("Category created successfully.");
            }catch (Exception ex)
            {
                return BadRequest(ex.Message); 
            }
        }

        // POST: Create SeasonProduct
        [HttpPost("/API/seasonproduct/create")]
        public IActionResult CreateSeasonProduct([FromBody] Product_Season ps)
        {
            try
            {
                _context.Product_Season.Add(ps);
                _context.SaveChanges();
                return Ok("Product_Season created successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: Create OrderItems
        [HttpPost("/API/product/create")]
        public IActionResult CreateProduct([FromBody] Product product)
        {
            try
            {
                if (!string.IsNullOrEmpty(product.priceAux))
                {
                    if (decimal.TryParse(product.priceAux, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal price))
                    {
                        product.price = price;
                    }
                    else
                    {
                        ModelState.AddModelError("", "O preço do produto é inválido.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Deve escolher o preço do produto, por favor.");
                }
                if (product.imagePath.StartsWith("data:image/"))
                {
                    int startIndex = product.imagePath.IndexOf("/") + 1; // Encontra a posição do primeiro caractere após "/"
                    int endIndex = product.imagePath.IndexOf(";"); // Encontra a posição do último caractere antes de ";"
                    string extFoto = product.imagePath.Substring(startIndex, endIndex - startIndex);

                    // Remova o prefixo "data:image/jpeg;base64," da string
                    string base64String = product.imagePath.Substring(product.imagePath.IndexOf(',') + 1);

                    // Decodifique a string base64 em um array de bytes
                    byte[] imagemBytes = Convert.FromBase64String(base64String);
                    //converter byte[] para ficheiro
                    string nomeArquivo = Guid.NewGuid().ToString() + "_" + product.id + "." + extFoto;
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", nomeArquivo);

                    using (FileStream fs = new FileStream(filePath, FileMode.Create))
                    {
                        fs.Write(imagemBytes, 0, imagemBytes.Length);
                    }
                    product.imagePath = nomeArquivo;
                }
                else
                {
                    //mesma imagem
                    product.imagePath = product.imagePath;
                }
                _context.Product.Add(product);
                _context.SaveChanges();
                return Ok("Product created successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: Create OrderItems
        [HttpPost("/API/Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] UserDTO userDTO)
        {
                var user = CreateUser();

                await _userStore.SetUserNameAsync(user, userDTO.email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, userDTO.email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, userDTO.password);

            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");
                Person p = new Person();
                p.userId = user.Id;
                p.dataNasc = userDTO.dataNasc;
                p.name = userDTO.name;
                p.address = userDTO.address;
                p.gender = userDTO.gender;
                p.phoneNumber = userDTO.phoneNumber;
                p.postalCode = userDTO.postalCode;
               
                try
                {
                    if (userDTO.imagePath!=null && userDTO.imagePath.StartsWith("data:image/"))
                    {
                        int startIndex = userDTO.imagePath.IndexOf("/") + 1; // Encontra a posição do primeiro caractere após "/"
                        int endIndex = userDTO.imagePath.IndexOf(";"); // Encontra a posição do último caractere antes de ";"
                        string extFoto = userDTO.imagePath.Substring(startIndex, endIndex - startIndex);

                        // Remova o prefixo "data:image/jpeg;base64," da string
                        string base64String = userDTO.imagePath.Substring(userDTO.imagePath.IndexOf(',') + 1);

                        // Decodifique a string base64 em um array de bytes
                        byte[] imagemBytes = Convert.FromBase64String(base64String);
                        //converter byte[] para ficheiro
                        string nomeArquivo = Guid.NewGuid().ToString() + "_" + p.userId + "." + extFoto;
                        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", nomeArquivo);

                        using (FileStream fs = new FileStream(filePath, FileMode.Create))
                        {
                            fs.Write(imagemBytes, 0, imagemBytes.Length);
                        }
                        p.imagePath = nomeArquivo;
                    }
                    else
                    {
                        if (p.gender == "M")
                        {
                            p.imagePath = "default-m.png";
                        }
                        else
                        {
                            p.imagePath = "default-f.png";
                        }
                    }
                    _context.Person.Add(p);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    _logger.LogError("Erro ao criar user");
                    throw;
                }
                var userId = await _userManager.GetUserIdAsync(user);
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                return Ok("Register created successfully.");
            }
            return BadRequest();
        }
        private IdentityUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<IdentityUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<IdentityUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<IdentityUser>)_userStore;
        }

    }
}
