using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DW_Final_Project.Data;
using DW_Final_Project.Models;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DW_Final_Project.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Product.Include(p => p.Season);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Season)
                .FirstOrDefaultAsync(m => m.id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            ViewData["seasonFK"] = new SelectList(_context.Product_Season, "id", "description");
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,description,quantity,priceAux,imagePath,seasonFK")] Product product, IFormFile imageFile)
        {

            // atribuir os dados do PrecoCompraAux ao PrecoCompra
            if (!string.IsNullOrEmpty(product.priceAux))
            {
                product.price =
                   Convert.ToDecimal(product.priceAux
                                            .Replace('.', ','));
            }
            else
            {
                ModelState.AddModelError("", "Deve escolher o preço do produto, por favor.");
            }
            ModelState.Remove("Season");
            ModelState.Remove("imageFile");
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    // Gerar um nome único para o arquivo
                    string fileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;

                    // Caminho completo para salvar a imagem (pode ser um caminho personalizado)
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

                    // Salvar o arquivo no servidor
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }

                    // Atualizar a propriedade imagePath do objeto person com o nome do arquivo
                    product.imagePath = fileName;
                }
                else
                {
                    product.imagePath = "default-c.png";
                }
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["seasonFK"] = new SelectList(_context.Product_Season, "id", "description", product.seasonFK);
            return View(product);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            product.priceAux = product.price.ToString();
            ViewData["seasonFK"] = new SelectList(_context.Product_Season, "id", "description", product.seasonFK);
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name,description,quantity,priceAux,imagePath,seasonFK")] Product product, IFormFile imageFile)
        {
            if (id != product.id)
            {
                return NotFound();
            }
            ModelState.Remove("Season");
            ModelState.Remove("imageFile");
            Product existingProduct = await _context.Product.FindAsync(id);
            existingProduct.id = product.id;
            existingProduct.name = product.name;
            existingProduct.description = product.description;
            existingProduct.quantity = product.quantity;
            // atribuir os dados do PrecoCompraAux ao PrecoCompra
            if (!string.IsNullOrEmpty(product.priceAux))
            {
                existingProduct.price =
                   Convert.ToDecimal(product.priceAux
                                            .Replace('.', ','));
            }
            else
            {
                ModelState.AddModelError("", "Deve escolher o preço do produto, por favor.");
            }


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(product).State = EntityState.Detached;
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        // Gerar um nome único para o arquivo
                        string fileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;

                        // Caminho completo para salvar a imagem (pode ser um caminho personalizado)
                        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

                        // Salvar o arquivo no servidor
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(fileStream);
                        }

                        // Atualizar a propriedade imagePath do objeto person com o nome do arquivo
                        existingProduct.imagePath = fileName;
                    }
                    _context.Update(existingProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["seasonFK"] = new SelectList(_context.Product_Season, "id", "description", product.seasonFK);
            return View(product);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Season)
                .FirstOrDefaultAsync(m => m.id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Product == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Product'  is null.");
            }
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Product?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
