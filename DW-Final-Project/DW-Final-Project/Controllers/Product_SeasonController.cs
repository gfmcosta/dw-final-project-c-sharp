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
    public class Product_SeasonController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Product_SeasonController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Product_Season
        public async Task<IActionResult> Index()
        {
              return _context.Product_Season != null ? 
                          View(await _context.Product_Season.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Product_Season'  is null.");
        }

        // GET: Product_Season/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Product_Season == null)
            {
                return NotFound();
            }

            var product_Season = await _context.Product_Season
                .FirstOrDefaultAsync(m => m.id == id);
            if (product_Season == null)
            {
                return NotFound();
            }

            return View(product_Season);
        }

        // GET: Product_Season/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Product_Season/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,description")] Product_Season product_Season)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product_Season);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product_Season);
        }

        // GET: Product_Season/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Product_Season == null)
            {
                return NotFound();
            }

            var product_Season = await _context.Product_Season.FindAsync(id);
            if (product_Season == null)
            {
                return NotFound();
            }
            return View(product_Season);
        }

        // POST: Product_Season/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,description")] Product_Season product_Season)
        {
            if (id != product_Season.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product_Season);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Product_SeasonExists(product_Season.id))
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
            return View(product_Season);
        }

        // GET: Product_Season/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Product_Season == null)
            {
                return NotFound();
            }

            var product_Season = await _context.Product_Season
                .FirstOrDefaultAsync(m => m.id == id);
            if (product_Season == null)
            {
                return NotFound();
            }

            return View(product_Season);
        }

        // POST: Product_Season/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Product_Season == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Product_Season'  is null.");
            }
            var product_Season = await _context.Product_Season.FindAsync(id);
            if (product_Season != null)
            {
                _context.Product_Season.Remove(product_Season);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Product_SeasonExists(int id)
        {
          return (_context.Product_Season?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
