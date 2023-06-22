using DW_Final_Project.Data;
using DW_Final_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DW_Final_Project.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public AdminController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            if (!User.Identity.Name.Contains("@admin.ipt.pt"))
            {
                return NotFound();
            }
            return View("~/Views/App/admin.cshtml");
        }

        public async Task<IActionResult> CategoriasAsync()
        {
            if (!User.Identity.Name.Contains("@admin.ipt.pt"))
            {
                return NotFound();
            }
            return _context.Category != null ?
                          View("~/Views/Category/index.cshtml", await _context.Category.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Category'  is null.");
        }

        public async Task<IActionResult> PedidosAsync()
        {
            if (!User.Identity.Name.Contains("@admin.ipt.pt"))
            {
                return NotFound();
            }
            var applicationDbContext = _context.Order.Include(o => o.Person);
            return View("~/Views/Order/index.cshtml", await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> ArtigosAsync()
        {
            if (!User.Identity.Name.Contains("@admin.ipt.pt"))
            {
                return NotFound();
            }
            var applicationDbContext = _context.OrderItem.Include(o => o.order).Include(o => o.product);
            return View("~/Views/OrderItem/index.cshtml", await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> PessoasAsync()
        {
            if (!User.Identity.Name.Contains("@admin.ipt.pt"))
            {
                return NotFound();
            }
            var applicationDbContext = _context.Person;
            return View("~/Views/Person/index.cshtml", await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> ProdutosAsync()
        {
            if (!User.Identity.Name.Contains("@admin.ipt.pt"))
            {
                return NotFound();
            }
            var applicationDbContext = _context.Product.Include(p => p.Season);
            return View("~/Views/Product/index.cshtml", await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> EpocasAsync()
        {
            if (!User.Identity.Name.Contains("@admin.ipt.pt"))
            {
                return NotFound();
            }
            return _context.Product_Season != null ?
                          View("~/Views/Product_Season/index.cshtml", await _context.Product_Season.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Product_Season'  is null.");
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