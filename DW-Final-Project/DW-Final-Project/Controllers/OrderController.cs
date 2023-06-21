using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DW_Final_Project.Data;
using DW_Final_Project.Models;
using DW_Final_Project.Migrations;
using System.Globalization;

namespace DW_Final_Project.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Order
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Order.Include(o => o.Person);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Order/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Person)
                .FirstOrDefaultAsync(m => m.id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Order/Create
        public IActionResult Create()
        {
            ViewData["personFK"] = new SelectList(_context.Person, "id", "name");
            return View();
        }

        // POST: Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,personFK")] Order order)
        {
            order.IVA = 23;
            order.price = 0;
            ModelState.Remove("priceAux");
            ModelState.Remove("Person");
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["personFK"] = new SelectList(_context.Person, "id", "name", order.personFK);
            return View(order);
        }

        // GET: Order/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            order.priceAux = order.price.ToString();
            ViewData["personFK"] = new SelectList(_context.Person, "id", "name", order.personFK);
            return View(order);
        }

        // POST: Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,priceAux,IVA,personFK")] Order order)
        {
                if (id != order.id)
            {
                return NotFound();
            }
            Order existingOrder = await _context.Order.FindAsync(id);
            existingOrder.id = order.id;
            existingOrder.IVA = order.IVA;
            existingOrder.personFK = order.personFK;
            // atribuir os dados do PrecoCompraAux ao PrecoCompra
            /*if (!string.IsNullOrEmpty(order.priceAux)) {
            
                existingOrder.price =
                   Convert.ToDecimal(order.priceAux
                                            .Replace('.', ','));
            }
            else
            {
                ModelState.AddModelError("", "Deve escolher o preço do produto, por favor.");
            }*/
            if (!string.IsNullOrEmpty(order.priceAux))
            {
                if (decimal.TryParse(order.priceAux, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal price))
                {
                    existingOrder.price = price;
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
            ModelState.Remove("Person");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(existingOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.id))
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
            ViewData["personFK"] = new SelectList(_context.Person, "id", "name", order.personFK);
            return View(order);
        }

        // GET: Order/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Person)
                .FirstOrDefaultAsync(m => m.id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Order == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Order'  is null.");
            }
            var order = await _context.Order.FindAsync(id);
            if (order != null)
            {
                _context.Order.Remove(order);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
          return (_context.Order?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
