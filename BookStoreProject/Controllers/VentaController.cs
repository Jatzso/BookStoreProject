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
    public class VentaController : Controller
    {
        private readonly BookStoreDBContext _context;

        public VentaController(BookStoreDBContext context)
        {
            _context = context;
        }

        // GET: Venta
        [Authorize(Roles = nameof(Rol.Administrador))]
        public async Task<IActionResult> Index()
        {
            var bookStoreDBContext = _context.Ventas.Include(v => v.Libro);
            ViewBag.TotalVentas = _context.Ventas.Count();
            ViewBag.TotalVendido = _context.Ventas.Select(v => v.Libro.Precio).Sum();
            return View(await bookStoreDBContext.ToListAsync());
        }

        // GET: Venta/Details/5
        [Authorize(Roles = nameof(Rol.Administrador))]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas
               .Include(v => v.Libro)
               .FirstOrDefaultAsync(m => m.Id == id);

            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }

        // GET: Venta/Create
        [Authorize(Roles = nameof(Rol.Administrador))]
        public IActionResult Create()
        {
            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Autor");
            return View();
        }

        // POST: Venta/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = nameof(Rol.Administrador))]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido,Dni,Calle,Altura,Provincia,Tarjeta,NumeroTarjeta,LibroId")] Venta venta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(venta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LibroId"] = new SelectList(_context.Libros, "Id", "Autor", venta.LibroId);
            return View(venta);
        }

        // GET: Venta/Delete/5
        [Authorize(Roles = nameof(Rol.Administrador))]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas
                .Include(v => v.Libro)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }

        // POST: Venta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = nameof(Rol.Administrador))]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venta = await _context.Ventas.FindAsync(id);
            _context.Ventas.Remove(venta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = nameof(Rol.Administrador))]
        private bool VentaExists(int id)
        {
            return _context.Ventas.Any(e => e.Id == id);
        }


        public ActionResult Comprar()
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult Comprar(int Id, string Nombre, string Apellido, int Dni, string Calle, int Altura,
            string Provincia, int Telefono, string Email, string Tarjeta, long NumeroTarjeta)
        {
            var venta = new Venta();
            venta.Nombre = Nombre;
            venta.Apellido = Apellido;
            venta.Dni = Dni;
            venta.Calle = Calle;
            venta.Altura = Altura;
            venta.Provincia = Provincia;
            venta.Telefono = Telefono;
            venta.Email = Email;
            venta.Tarjeta = Tarjeta;
            venta.NumeroTarjeta = NumeroTarjeta;
            venta.LibroId = Id;
            var libro = _context.Libros.Find(Id);
            venta.Libro = libro;
            venta.Fecha = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Ventas.Add(venta);
            _context.SaveChanges();
                ViewBag.LibroId = venta.LibroId;
                return View("CompraExitosa");

            }
            
            return RedirectToAction("Index", "Home");
        }
        
    }
}
