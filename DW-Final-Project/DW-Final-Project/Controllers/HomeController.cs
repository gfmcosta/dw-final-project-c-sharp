using DW_Final_Project.Data;
using DW_Final_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;

namespace DW_Final_Project.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        public static List<OrderItem> carrinho;
        public static List<Product> produto;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
		{
			_logger = logger;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
		}

		public IActionResult Index()
		{
            ViewBag.user = User.Identity;
            if (User.Identity.IsAuthenticated)
            {
            if (User.Identity.Name.Contains("@admin.ipt.pt"))
            {
                //return View("~/Views/App/admin.cshtml");
                return RedirectToAction("Index","Admin");
            }
            }
            return View("~/Views/App/splash.cshtml");
        }

        public async Task<IActionResult> ProdutosAsync()
        {
            var products = await _context.Product.ToListAsync();
            produto = products;
            ViewBag.Products = products;
            return View("~/Views/App/produtos.cshtml");
        }

        public async Task<IActionResult> ProdutoInfoAsync(int id)
        {
            var id_produto = id;
            var products = await _context.Product.FirstOrDefaultAsync(p => p.id == id_produto);
            ViewBag.Products = products;
            return View("~/Views/App/produtoInfo.cshtml");
        }


        [HttpPost]
        public async Task<IActionResult> AddCartAsync(string size, int quantity,decimal productPrice, int productFK, string submitAction)
        {
            string teste = "[]" ;
            if (TempData["shoppingCart"] != null)
            {
                teste = TempData["shoppingCart"].ToString();
            }
            OrderItem item = new OrderItem();
            item.size=size;
            item.quantity=quantity;
            item.productFK=productFK;
            item.totalPrice = quantity * productPrice;
            string serializedList = teste;
            List<OrderItem> lista = JsonConvert.DeserializeObject<List<OrderItem>>(serializedList);
            lista.Add(item);
            carrinho = lista;
            if (submitAction == "Adicionar")
            {
                string listaJson = JsonConvert.SerializeObject(lista);
                TempData["shoppingCart"]= listaJson;
            }

            var products = await _context.Product.ToListAsync();
            ViewBag.Products = products;
            return View("~/Views/App/produtos.cshtml");
        }

        public IActionResult Sobre()
        {
            return View("~/Views/App/sobre.cshtml");
        }

        public IActionResult Login()
        {
            return View("~/Areas/Identity/Pages/Account/Login.cshtml");
        }

        public IActionResult Registar()
        {
            return View("~/Views/App/register.cshtml");
        }

        public IActionResult Privacy()
		{
			return View();
		}

        public async Task<IActionResult> PedidosAsync(int id, string orderItemList)
        {
            var orderItems = Newtonsoft.Json.JsonConvert.DeserializeObject<List<OrderItem>>(orderItemList);

            var order = new Order
            {
                price = 0,
                IVA = 23,
                personFK = id,
                date = DateTime.Now
            };

            _context.Order.Add(order);
            _context.SaveChanges();

            foreach (var orderItemModel in orderItems)
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

            carrinho = new List<OrderItem>();
            TempData["shoppingCart"] = null;
            return View("~/Views/App/Splash.cshtml");
        }

        public async Task<IActionResult> CarrinhoAsync()
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            var pid = await _context.Person.Where(p => p.userId == user.Id).FirstOrDefaultAsync();
            ViewBag.pessoa = pid;
            ViewBag.carrinho = carrinho;
            ViewBag.Produto = produto;
            return View("~/Views/App/carrinho.cshtml");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}