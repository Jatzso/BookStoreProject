using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
namespace BookStoreProject.Models
{
    public class Libro
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required(ErrorMessage ="El campo es requerido")]
        public string Titulo { get; set; }
        
        [Required(ErrorMessage = "El campo es requerido")]
        public string Autor { get; set; }
        
        [Required(ErrorMessage = "El campo es requerido")]
        public string Editorial { get; set; }
       
        [Required(ErrorMessage = "El campo es requerido")]
        public string Genero { get; set; }
       
        [Required(ErrorMessage = "El campo es requerido")]
        [Range(1,1000000, ErrorMessage = "El precio debe ser mayor a 0")]
        public int Precio { get; set; }

    }
}