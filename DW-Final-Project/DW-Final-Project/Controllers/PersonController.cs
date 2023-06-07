using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DW_Final_Project.Data;
using DW_Final_Project.Models;

namespace DW_Final_Project.Controllers {
    public class PersonController : Controller {
        private readonly ApplicationDbContext _context;

        public PersonController(ApplicationDbContext context) {
            _context = context;
        }

        // GET: Person
        public async Task<IActionResult> Index() {
            var applicationDbContext = _context.Person.Include(p => p.user);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Person/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null || _context.Person == null) {
                return NotFound();
            }

            var person = await _context.Person
                .Include(p => p.user)
                .FirstOrDefaultAsync(m => m.id == id);
            if (person == null) {
                return NotFound();
            }

            return View(person);
        }

        // GET: Person/Create
        public IActionResult Create() {
            ViewData["userFK"] = new SelectList(_context.User, "id", "email");
            return View();
        }

        // POST: Person/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,phoneNumber,address,postalCode,dataNasc,gender,imagePath,userFK")] Person person, IFormFile imageFile) {
            ModelState.Remove("user");
            ModelState.Remove("imageFile");
            ModelState.Remove("imagePath");
            User user = await _context.User.FindAsync(person.userFK);
            person.user = user;
            if (ModelState.IsValid) {
                if (imageFile != null && imageFile.Length > 0) {
                    // Gerar um nome único para o arquivo
                    string fileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;

                    // Caminho completo para salvar a imagem (pode ser um caminho personalizado)
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

                    // Salvar o arquivo no servidor
                    using (var fileStream = new FileStream(filePath, FileMode.Create)) {
                        await imageFile.CopyToAsync(fileStream);
                    }

                    // Atualizar a propriedade imagePath do objeto person com o nome do arquivo
                    person.imagePath = fileName;
                } else {
                    if (person.gender == "M") {
                        person.imagePath = "default-m.png";
                    } else {
                        person.imagePath = "default-f.png";
                    }
                }

                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["userFK"] = new SelectList(_context.User, "id", "email", person.userFK);
            return View(person);
        }


        // GET: Person/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null || _context.Person == null) {
                return NotFound();
            }

            var person = await _context.Person.FindAsync(id);
            if (person == null) {
                return NotFound();
            }
            ViewData["userFK"] = new SelectList(_context.User, "id", "email", person.userFK);
            return View(person);
        }

        // POST: Person/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name,phoneNumber,address,postalCode,dataNasc,gender,imagePath,userFK")] Person person, IFormFile imageFile) {
            if (id != person.id) {
                return NotFound();
            }
            ModelState.Remove("user");
            ModelState.Remove("imageFile");
            ModelState.Remove("imagePath");
            User user = await _context.User.FindAsync(person.userFK);
            Person existingPerson = await _context.Person.FindAsync(id);

            // Copiar as propriedades atualizadas para a entidade existente
            existingPerson.id = person.id;
            existingPerson.name = person.name;
            existingPerson.phoneNumber = person.phoneNumber;
            existingPerson.address = person.address;
            existingPerson.postalCode = person.postalCode;
            existingPerson.dataNasc = person.dataNasc;
            existingPerson.gender = person.gender;
            existingPerson.userFK = person.userFK;
            existingPerson.user = user;


            if (ModelState.IsValid) {
                try {

                    _context.Entry(person).State = EntityState.Detached;
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
                        existingPerson.imagePath = fileName;
                    }
                    _context.Update(existingPerson);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!PersonExists(person.id)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["userFK"] = new SelectList(_context.User, "id", "email", person.userFK);
            return View(person);
        }

        // GET: Person/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null || _context.Person == null) {
                return NotFound();
            }

            var person = await _context.Person
                .Include(p => p.user)
                .FirstOrDefaultAsync(m => m.id == id);
            if (person == null) {
                return NotFound();
            }

            return View(person);
        }

        // POST: Person/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            if (_context.Person == null) {
                return Problem("Entity set 'ApplicationDbContext.Person'  is null.");
            }
            var person = await _context.Person.FindAsync(id);
            if (person != null) {
                _context.Person.Remove(person);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id) {
            return (_context.Person?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}