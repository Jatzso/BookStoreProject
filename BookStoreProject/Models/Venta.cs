using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreProject.Models
{
    public class Venta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "El campo es requerido")]
        public string Nombre { get; set; }
        
        [Required(ErrorMessage = "El campo es requerido")]
        public string Apellido { get; set; }
        
        [Required]
        [Range(1000000, 990000000)]
        public int Dni { get; set; }
        
        [Required]
        public string Calle { get; set; }

        [Required]
        public int Altura { get; set; }

        [Required]
        public string Provincia { get; set; }

        [Required]
        public string Tarjeta { get; set; }

        [Required]
        public int NumeroTarjeta { get; set; }

        //Relaciones con otras entidades
        [ForeignKey(nameof(Libro))]
        public int LibroId { get; set; }
        public Libro Libro { get; set; }


    }
}
