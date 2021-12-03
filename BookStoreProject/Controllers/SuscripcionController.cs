using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStoreProject.Context;
using BookStoreProject.Models;

namespace BookStoreProject.Controllers
{
    public class SuscripcionController : Controller
    {
        private readonly BookStoreDBContext _context;

        public SuscripcionController(BookStoreDBContext context)
        {
            _context = context;
        }

        // GET: Suscripcion
        public async Task<IActionResult> Index()
        {
            var bookStoreDBContext = _context.Suscripciones.Include(s => s.Usuario);
            return View(await bookStoreDBContext.ToListAsync());
        }

        // GET: Suscripcion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suscripcion = await _context.Suscripciones
                .Include(s => s.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (suscripcion == null)
            {
                return NotFound();
            }

            return View(suscripcion);
        }

        // GET: Suscripcion/Create
        public IActionResult Create()
        {
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id");
            return View();
        }

        // POST: Suscripcion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,UsuarioId")] Suscripcion suscripcion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(suscripcion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", suscripcion.UsuarioId);
            return View(suscripcion);
        }

        // GET: Suscripcion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suscripcion = await _context.Suscripciones.FindAsync(id);
            if (suscripcion == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", suscripcion.UsuarioId);
            return View(suscripcion);
        }

        // POST: Suscripcion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,UsuarioId")] Suscripcion suscripcion)
        {
            if (id != suscripcion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(suscripcion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SuscripcionExists(suscripcion.Id))
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
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", suscripcion.UsuarioId);
            return View(suscripcion);
        }

        // GET: Suscripcion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suscripcion = await _context.Suscripciones
                .Include(s => s.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (suscripcion == null)
            {
                return NotFound();
            }

            return View(suscripcion);
        }

        // POST: Suscripcion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var suscripcion = await _context.Suscripciones.FindAsync(id);
            _context.Suscripciones.Remove(suscripcion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SuscripcionExists(int id)
        {
            return _context.Suscripciones.Any(e => e.Id == id);
        }

        public ActionResult Suscribirse()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Suscribirse(Suscripcion suscripcion)
        {
            if (ModelState.IsValid)
            {
                if (!_context.Suscripciones.Any(s => s.Email == suscripcion.Email))
                {
                    var userName = User.Identity.Name;
                    var user = _context.Usuarios.FirstOrDefault(u => u.User == userName);
                    suscripcion.UsuarioId = user.Id;
                    suscripcion.Usuario = user;
                    suscripcion.Fecha = DateTime.Now;
                    _context.Suscripciones.Add(suscripcion);
                    _context.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(nameof(Suscripcion.Email), "El correo ya se encuentra suscripto");
                }
                
            }
            return View();
        }
    }
}
