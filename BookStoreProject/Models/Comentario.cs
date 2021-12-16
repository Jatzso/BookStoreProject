using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreProject.Models
{
    public class Comentario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [MaxLength(80, ErrorMessage = "*Longitud máxima {1}")]
        [Display(Name = "Comentario")]
        [Required]
        public string Comment { get; set; }

        //Relaciones con otras entidades
        
        [ForeignKey(nameof(Libro))]
      
        public int LibroId { get; set; }
       
        public Libro Libro { get; set; }

        [ForeignKey(nameof(Usuario))]
        [Required]
        public int UsuarioId { get; set; }
        [Required]
        public Usuario Usuario { get; set; }
    }
}
