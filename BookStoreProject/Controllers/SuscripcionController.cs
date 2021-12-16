using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStoreProject.Context;
using BookStoreProject.Models;
using Microsoft.AspNetCore.Authorization;

namespace BookStoreProject.Controllers
{
    [Authorize]
    public class SuscripcionController : Controller
    {
        private readonly BookStoreDBContext _context;

        public SuscripcionController(BookStoreDBContext context)
        {
            _context = context;
        }

        public ActionResult Index(string cadenaBuscada)
        {

            var suscripciones = from s in _context.Suscripciones.Include(s => s.Usuario)
                             select s;

            if (!String.IsNullOrEmpty(cadenaBuscada))
            {
                suscripciones = suscripciones.Where(s => s.Email.Contains(cadenaBuscada));
            }

            return View(suscripciones);
        }


        // GET: Suscripcion/Create
        [Authorize(Roles = nameof(Rol.Administrador))]
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
        [Authorize(Roles = nameof(Rol.Administrador))]
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


        // GET: Suscripcion/Delete/5
        [Authorize(Roles = nameof(Rol.Administrador))]
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
        [Authorize(Roles = nameof(Rol.Administrador))]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var suscripcion = await _context.Suscripciones.FindAsync(id);
            _context.Suscripciones.Remove(suscripcion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = nameof(Rol.Administrador))]
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
                    return View("SuscripcionExitosa");
                }
                else
                {
                    ModelState.AddModelError(nameof(Suscripcion.Email), "*Correo ya suscripto");
                }
                
            }
            return View();
        }
    }
}
