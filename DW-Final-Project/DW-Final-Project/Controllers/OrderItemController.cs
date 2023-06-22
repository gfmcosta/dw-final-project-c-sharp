using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DW_Final_Project.Data;
using DW_Final_Project.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authorization;

namespace DW_Final_Project.Controllers
{
    [Authorize]
    public class OrderItemController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OrderItem
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.Name.Contains("@admin.ipt.pt"))
            {
                return NotFound();
            }
            var applicationDbContext = _context.OrderItem.Include(o => o.order).Include(o => o.product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: OrderItem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!User.Identity.Name.Contains("@admin.ipt.pt"))
            {
                return NotFound();
            }
            if (id == null || _context.OrderItem == null)
            {
                return NotFound();
            }

            var orderItem = await _context.OrderItem
                .Include(o => o.order)
                .Include(o => o.product)
                .FirstOrDefaultAsync(m => m.id == id);
            if (orderItem == null)
            {
                return NotFound();
            }

            return View(orderItem);
        }

        // GET: OrderItem/Create
        public IActionResult Create()
        {
            if (!User.Identity.Name.Contains("@admin.ipt.pt"))
            {
                return NotFound();
            }
            ViewData["orderFK"] = new SelectList(_context.Order, "id", "id");
            ViewData["productFK"] = new SelectList(_context.Product, "id", "name");
            return View();
        }

        // POST: OrderItem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("size,quantity,orderFK,productFK")] OrderItem orderItem)
        {
            if (!User.Identity.Name.Contains("@admin.ipt.pt"))
            {
                return NotFound();
            }
            ModelState.Remove("order");
            ModelState.Remove("product");
            ModelState.Remove("totalPriceAux");
            Product p = await _context.Product.FindAsync(orderItem.productFK);
            orderItem.totalPrice = p.price * orderItem.quantity;
            if (ModelState.IsValid)
            {
                _context.Add(orderItem);
                await _context.SaveChangesAsync();
                Order o = await _context.Order.FindAsync(orderItem.orderFK);
                o.price += orderItem.totalPrice;
                _context.Update(o);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["orderFK"] = new SelectList(_context.Order, "id", "id", orderItem.orderFK);
            ViewData["productFK"] = new SelectList(_context.Product, "id", "name", orderItem.productFK);
            return View(orderItem);
        }

        // GET: OrderItem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!User.Identity.Name.Contains("@admin.ipt.pt"))
            {
                return NotFound();
            }
            if (id == null || _context.OrderItem == null)
            {
                return NotFound();
            }

            var orderItem = await _context.OrderItem.FindAsync(id);
            if (orderItem == null)
            {
                return NotFound();
            }
            ViewData["orderFK"] = new SelectList(_context.Order, "id", "id", orderItem.orderFK);
            ViewData["productFK"] = new SelectList(_context.Product, "id", "name", orderItem.productFK);
            return View(orderItem);
        }

        // POST: OrderItem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,size,quantity,productFK")] OrderItem orderItem)
        {
            if (!User.Identity.Name.Contains("@admin.ipt.pt"))
            {
                return NotFound();
            }
            if (id != orderItem.id)
            {
                return NotFound();
            }
            OrderItem olderItem = await _context.OrderItem.FindAsync(id);
            decimal precoApagar = olderItem.totalPrice;
            OrderItem existingOrderItem = await _context.OrderItem.FindAsync(id);
            existingOrderItem.quantity = orderItem.quantity;
            existingOrderItem.size = orderItem.size;
            existingOrderItem.orderFK = olderItem.orderFK;
            existingOrderItem.productFK = orderItem.productFK;
            existingOrderItem.quantity = orderItem.quantity;
            Product oldP = await _context.Product.FindAsync(olderItem.productFK);
            Product newP = await _context.Product.FindAsync(existingOrderItem.productFK);
            existingOrderItem.totalPrice = orderItem.quantity * newP.price;
            decimal preco = oldP.price * olderItem.quantity;
            ModelState.Remove("order");
            ModelState.Remove("product");
            ModelState.Remove("totalPriceAux");
            if (ModelState.IsValid)
            {
                try
                {
                    Order o = await _context.Order.FindAsync(existingOrderItem.orderFK);
                    o.price -= precoApagar;
                    o.price += existingOrderItem.totalPrice;
                    _context.Update(existingOrderItem);
                    await _context.SaveChangesAsync();
                    _context.Update(o);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderItemExists(orderItem.id))
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
            ViewData["orderFK"] = new SelectList(_context.Order, "id", "id", orderItem.orderFK);
            ViewData["productFK"] = new SelectList(_context.Product, "id", "name", orderItem.productFK);
            return View(orderItem);
        }

        // GET: OrderItem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!User.Identity.Name.Contains("@admin.ipt.pt"))
            {
                return NotFound();
            }
            if (id == null || _context.OrderItem == null)
            {
                return NotFound();
            }

            var orderItem = await _context.OrderItem
                .Include(o => o.order)
                .Include(o => o.product)
                .FirstOrDefaultAsync(m => m.id == id);
            if (orderItem == null)
            {
                return NotFound();
            }

            return View(orderItem);
        }

        // POST: OrderItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!User.Identity.Name.Contains("@admin.ipt.pt"))
            {
                return NotFound();
            }
            if (_context.OrderItem == null)
            {
                return Problem("Entity set 'ApplicationDbContext.OrderItem'  is null.");
            }
            var orderItem = await _context.OrderItem.FindAsync(id);
            if (orderItem != null)
            {
                _context.OrderItem.Remove(orderItem);
                Order o = await _context.Order.FindAsync(orderItem.orderFK);
                o.price -= orderItem.totalPrice;
                _context.Order.Update(o);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderItemExists(int id)
        {
            return (_context.OrderItem?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
