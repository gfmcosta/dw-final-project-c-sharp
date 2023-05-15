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
    public class TypeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Type
        public async Task<IActionResult> Index()
        {
              return _context.Type != null ? 
                          View(await _context.Type.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Type'  is null.");
        }

        // GET: Type/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Type == null)
            {
                return NotFound();
            }

            var @type = await _context.Type
                .FirstOrDefaultAsync(m => m.id == id);
            if (@type == null)
            {
                return NotFound();
            }

            return View(@type);
        }

        // GET: Type/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Type/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,description")] Models.Type @type)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@type);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@type);
        }

        // GET: Type/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Type == null)
            {
                return NotFound();
            }

            var @type = await _context.Type.FindAsync(id);
            if (@type == null)
            {
                return NotFound();
            }
            return View(@type);
        }

        // POST: Type/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,description")] Models.Type @type)
        {
            if (id != @type.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@type);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeExists(@type.id))
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
            return View(@type);
        }

        // GET: Type/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Type == null)
            {
                return NotFound();
            }

            var @type = await _context.Type
                .FirstOrDefaultAsync(m => m.id == id);
            if (@type == null)
            {
                return NotFound();
            }

            return View(@type);
        }

        // POST: Type/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Type == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Type'  is null.");
            }
            var @type = await _context.Type.FindAsync(id);
            if (@type != null)
            {
                _context.Type.Remove(@type);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeExists(int id)
        {
          return (_context.Type?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
