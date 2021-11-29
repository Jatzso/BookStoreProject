using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreProject.Models;

namespace BookStoreProject.Context
{
    public class BookStoreDBContext : DbContext
    {
        public BookStoreDBContext(DbContextOptions<BookStoreDBContext> options) : base(options)
        {
        }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<BookStoreProject.Models.Sucursal> Sucursal { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }
       
        public DbSet<Suscripcion> Suscripciones { get; set; }   
        public DbSet<Venta> Ventas { get; set; }
        
        public DbSet<Comentario>Comentarios { get; set; }
                        

      

        

    }
}