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
    public class CompraController : Controller
    {
        private readonly BookStoreDBContext _context;

        public CompraController(BookStoreDBContext context)
        {
            _context = context;
        }

        // GET: Compra
        [Authorize(Roles = nameof(Rol.Administrador))]
        public async Task<IActionResult> Index()
        {
            var bookStoreDBContext = _context.Compras.Include(c => c.Libro).Include(c => c.Usuario);
            return View(await bookStoreDBContext.ToListAsync());
        }

        // GET: Compra/Details/5
        [Authorize(Roles = nameof(Rol.Administrador))]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras
                .Include(c => c.Libro)
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (compra == null)
            {
                return NotFound();
            }

            return View(compra);
        }

        // GET: Compra/Create
        [Authorize(Roles = nameof(Rol.Administrador))]
        public IActionResult Create()
        {
            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Autor");
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id");
            return View();
        }

        // POST: Compra/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = nameof(Rol.Administrador))]
        public async Task<IActionResult> Create([Bind("Id,LibroId,UsuarioId")] Compra compra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(compra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Autor", compra.LibroId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", compra.UsuarioId);
            return View(compra);
        }

        // GET: Compra/Edit/5
        [Authorize(Roles = nameof(Rol.Administrador))]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras.FindAsync(id);
            if (compra == null)
            {
                return NotFound();
            }
            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Autor", compra.LibroId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", compra.UsuarioId);
            return View(compra);
        }

        // POST: Compra/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = nameof(Rol.Administrador))]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LibroId,UsuarioId")] Compra compra)
        {
            if (id != compra.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(compra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompraExists(compra.Id))
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
            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Autor", compra.LibroId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", compra.UsuarioId);
            return View(compra);
        }

        // GET: Compra/Delete/5
        [Authorize(Roles = nameof(Rol.Administrador))]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras
                .Include(c => c.Libro)
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (compra == null)
            {
                return NotFound();
            }

            return View(compra);
        }

        // POST: Compra/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = nameof(Rol.Administrador))]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var compra = await _context.Compras.FindAsync(id);
            _context.Compras.Remove(compra);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompraExists(int id)
        {
            return _context.Compras.Any(e => e.Id == id);
        }
    }
}
