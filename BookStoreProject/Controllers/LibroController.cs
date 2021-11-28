﻿using System;
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
    public class LibroController : Controller
    {
        private readonly BookStoreDBContext _context;

        public LibroController(BookStoreDBContext context)
        {
            _context = context;
        }

        //Busqueda prueba

        public ActionResult Index(string searchString)
        {

            var libros = from l in _context.Libros
                         select l;

            if (!String.IsNullOrEmpty(searchString))
            {
                libros = libros.Where(s => s.Titulo.Contains(searchString));
            }

            return View(libros);
        }


        //**************************************************
        /*
        // GET: Libro
        public async Task<IActionResult> Index()
        {
            return View(await _context.Libros.ToListAsync());
        }
        */

        // GET: Libro/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros
                .FirstOrDefaultAsync(m => m.Id == id);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // GET: Libro/Create
        [Authorize(Roles = nameof(Rol.Administrador))]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Libro/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = nameof(Rol.Administrador))]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Autor,Editorial,Genero,Precio")] Libro libro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(libro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(libro);
        }

        // GET: Libro/Edit/5
        [Authorize(Roles = nameof(Rol.Administrador))]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros.FindAsync(id);
            if (libro == null)
            {
                return NotFound();
            }
            return View(libro);
        }

        // POST: Libro/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = nameof(Rol.Administrador))]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Autor,Editorial,Genero,Precio")] Libro libro)
        {
            if (id != libro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(libro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibroExists(libro.Id))
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
            return View(libro);
        }

        // GET: Libro/Delete/5
        [Authorize(Roles = nameof(Rol.Administrador))]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros
                .FirstOrDefaultAsync(m => m.Id == id);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // POST: Libro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = nameof(Rol.Administrador))]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var libro = await _context.Libros.FindAsync(id);
            _context.Libros.Remove(libro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LibroExists(int id)
        {
            return _context.Libros.Any(e => e.Id == id);
        }

        /*************************************************************/

        public async Task<IActionResult> Comprar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros
                .FirstOrDefaultAsync(m => m.Id == id);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }
        [HttpGet]
        public ActionResult Confirmar(int? id, string Titulo)
        {
            var libro = _context.Libros.First(l => l.Id == id);

            if(libro != null)
            {
                var nuevaCompra = new Compra();
                nuevaCompra.LibroId = libro.Id;
                var name = User.Identity.Name;
                var usuario = _context.Usuarios.First(u => u.User.Equals(name));
                nuevaCompra.UsuarioId = usuario.Id;
                _context.Compras.Add(nuevaCompra);
                _context.Libros.Remove(libro);
                _context.SaveChanges();

            }
            else
            {
                return NotFound();
            }

            return View("Home", "Index");

            /***********************************************************/


        }
    }
}

   

