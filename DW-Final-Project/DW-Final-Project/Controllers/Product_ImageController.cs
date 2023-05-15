using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DW_Final_Project.Data;
using DW_Final_Project.Models;

namespace DW_Final_Project.Controllers
{
    public class Product_ImageController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Product_ImageController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Product_Image
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Product_Image.Include(p => p.product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Product_Image/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Product_Image == null)
            {
                return NotFound();
            }

            var product_Image = await _context.Product_Image
                .Include(p => p.product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product_Image == null)
            {
                return NotFound();
            }

            return View(product_Image);
        }

        // GET: Product_Image/Create
        public IActionResult Create()
        {
            ViewData["productFK"] = new SelectList(_context.Product, "id", "id");
            return View();
        }

        // POST: Product_Image/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,imagePath,productFK")] Product_Image product_Image)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product_Image);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["productFK"] = new SelectList(_context.Product, "id", "id", product_Image.productFK);
            return View(product_Image);
        }

        // GET: Product_Image/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Product_Image == null)
            {
                return NotFound();
            }

            var product_Image = await _context.Product_Image.FindAsync(id);
            if (product_Image == null)
            {
                return NotFound();
            }
            ViewData["productFK"] = new SelectList(_context.Product, "id", "id", product_Image.productFK);
            return View(product_Image);
        }

        // POST: Product_Image/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,imagePath,productFK")] Product_Image product_Image)
        {
            if (id != product_Image.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product_Image);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Product_ImageExists(product_Image.Id))
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
            ViewData["productFK"] = new SelectList(_context.Product, "id", "id", product_Image.productFK);
            return View(product_Image);
        }

        // GET: Product_Image/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Product_Image == null)
            {
                return NotFound();
            }

            var product_Image = await _context.Product_Image
                .Include(p => p.product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product_Image == null)
            {
                return NotFound();
            }

            return View(product_Image);
        }

        // POST: Product_Image/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Product_Image == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Product_Image'  is null.");
            }
            var product_Image = await _context.Product_Image.FindAsync(id);
            if (product_Image != null)
            {
                _context.Product_Image.Remove(product_Image);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Product_ImageExists(int id)
        {
          return (_context.Product_Image?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
