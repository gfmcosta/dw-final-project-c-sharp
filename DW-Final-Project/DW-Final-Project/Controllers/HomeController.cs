using DW_Final_Project.Data;
using DW_Final_Project.Models;
using Microsoft.AspNetCore.Http;
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

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
		{
			_logger = logger;
            _context = context;
		}

		public IActionResult Index()
		{
            return View("~/Views/App/perfil.cshtml");
        }

        public async Task<IActionResult> ProdutosAsync()
        {
            var products = await _context.Product.ToListAsync();
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
        public async Task<IActionResult> AddCartAsync(string size, [Bind("id,productFK,quantity,priceAux,imagePath")] OrderItem item, string submitAction)
        {
            string serializedList = HttpContext.Session.GetString("shoppingCart");
            List<OrderItem> lista = JsonConvert.DeserializeObject<List<OrderItem>>(serializedList);
            lista.Add(item);
            if (submitAction == "Adicionar"){
                string listaJson = JsonConvert.SerializeObject(lista);
                HttpContext.Session.SetString("shoppingCart", listaJson);
            }
            return View();
        }

        public IActionResult Sobre()
        {
            return View("~/Views/App/sobre.cshtml");
        }

        public IActionResult Login()
        {
            return View("~/Views/App/login.cshtml");
        }

        public IActionResult Registar()
        {
            return View("~/Views/App/register.cshtml");
        }

        public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}