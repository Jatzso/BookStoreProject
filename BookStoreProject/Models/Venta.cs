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
        
        [Required(ErrorMessage = "*Campo requerido")]
        [RegularExpression(@"[A-Za-z]{3,}", ErrorMessage = "*Campo inválido")]
        public string Nombre { get; set; }
        
        [Required(ErrorMessage = "*Campo requerido")]
        [RegularExpression(@"[A-Za-z]{3,}", ErrorMessage = "*Campo inválido")]
        public string Apellido { get; set; }
        
        [Required(ErrorMessage = "*Campo requerido")]
        [Range(1000000, 990000000)]
        [RegularExpression(@"[0-9]{8}", ErrorMessage = "*DNI inválido")]
        public int Dni { get; set; }
        
        [Required(ErrorMessage = "*Campo requerido")]
        [RegularExpression(@"[A-Za-z ]{3,}", ErrorMessage = "*Calle inválida")]
        public string Calle { get; set; }

        [Required(ErrorMessage = "*Campo requerido")]
        [RegularExpression(@"[0-9]{1,4}", ErrorMessage = "*Altura inválida")]
        public int Altura { get; set; }

        [Required(ErrorMessage = "*Campo requerido")]
        [RegularExpression(@"[A-Za-z ]{4,}", ErrorMessage = "*Provincia inválida")]
        public string Provincia { get; set; }
        
        [Required(ErrorMessage = "*Campo requerido")]
        [Phone]
        [RegularExpression(@"11[0-9]{8}", ErrorMessage = "*Teléfono inválido")]
        public int Telefono { get; set; }

        [Required(ErrorMessage = "*Campo requerido")]
        [EmailAddress(ErrorMessage = "*Email inválido")]
        [RegularExpression(@"^[^@]+@[^@]+\.[a-zA-Z]{2,}$", ErrorMessage = "*Formato inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "*Campo requerido")]
        public string Tarjeta { get; set; }

        [Required(ErrorMessage = "*Campo requerido")]
        [CreditCard]
        [RegularExpression(@"5[1-5][0-9]{14}$", ErrorMessage = "*Nro° tarjeta inválida")]
        public int NumeroTarjeta { get; set; }

        //Relaciones con otras entidades
       
        [ForeignKey(nameof(Libro))]
        public int LibroId { get; set; }
        public Libro Libro { get; set; }


    }
}
