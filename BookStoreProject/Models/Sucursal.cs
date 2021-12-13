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
        
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Nombre { get; set; }
        
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Direccion { get; set; }
        
        [Required(ErrorMessage = "Este campo es requerido")]
        [Phone]
        public string Telefono { get; set; }
        
        [Required(ErrorMessage = "Este campo es requerido")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Abierto")]
        public int HoraApertura { get; set; }
       
        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Cerrado")]
        public int HoraCierre { get; set; }
    }
}
