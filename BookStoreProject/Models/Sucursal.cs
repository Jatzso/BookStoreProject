using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreProject.Models
{
    public class Sucursal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "*Campo requerido")]
        [MaxLength(40, ErrorMessage = "*Longitud maxima {1}")]
        public string Nombre { get; set; }
        
        [Required(ErrorMessage = "*Campo requerido")]
        [MaxLength(40, ErrorMessage = "*Longitud maxima {1}")]
        public string Direccion { get; set; }
        
        [Required(ErrorMessage = "*Campo requerido")]
        [Phone]
        [RegularExpression(@"4[3-9][0-9]{6}", ErrorMessage = "*Formato inválido")]
        public string Telefono { get; set; }
        
        [Required(ErrorMessage = "*Campo requerido")]
        [EmailAddress(ErrorMessage = "*Formato inválido")]
        [Display(Name = "Email")]
        [RegularExpression(@"^[^@]+@[^@]+\.[a-zA-Z]{2,}$", ErrorMessage = "*Formato inválido")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Abierto")]
        [Range(1, 24, ErrorMessage = "*Horarario inválido")]
        public int HoraApertura { get; set; }
       
        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Cerrado")]
        [Range(1, 24, ErrorMessage = "*Horarario inválido")]
        public int HoraCierre { get; set; }
    }
}
