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
        
        [Required(ErrorMessage ="*Campo requerido")]
        [MaxLength(40, ErrorMessage ="*Longitud maxima {1}")]
        public string Titulo { get; set; }
        
        [Required(ErrorMessage = "*Campo requerido")]
        [MaxLength(40, ErrorMessage = "*Longitud maxima {1}")]
        [RegularExpression(@"[a-zA-z ]{3,}", ErrorMessage = "*Campo con números")]
        public string Autor { get; set; }
        
        [Required(ErrorMessage = "*Campo requerido")]
        [MaxLength(40, ErrorMessage = "*Longitud maxima {1}")]
        [RegularExpression(@"[a-zA-z ]{3,}", ErrorMessage = "*Campo con números")]
        public string Editorial { get; set; }
       
        [Required(ErrorMessage = "*Campo requerido")]
        [MaxLength(40, ErrorMessage = "*Longitud maxima {1}")]
        [RegularExpression(@"[a-zA-z ]{3,}", ErrorMessage = "*Campo con números")]
        public string Genero { get; set; }
       
        [Required(ErrorMessage = "*Campo requerido")]
        [Range(1,1000000, ErrorMessage = "*Precio menor a 1")]
        public int Precio { get; set; }

    }
}