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
        [RegularExpression(@"[A-Za-z]{3,}", ErrorMessage = "El nombre debe contener al menos 3 caracteres")]
        public string Nombre { get; set; }
        
        [Required(ErrorMessage = "El campo es requerido")]
        [RegularExpression(@"[A-Za-z]{3,}", ErrorMessage = "El apellido debe contener al menos 3 caracteres")]
        public string Apellido { get; set; }
        
        [Required(ErrorMessage = "El campo es requerido")]
        [Range(1000000, 990000000)]
        [RegularExpression(@"[0-9]{8}", ErrorMessage = "El DNI ingresado es inválido")]
        public int Dni { get; set; }
        
        [Required(ErrorMessage = "El campo es requerido")]
        [RegularExpression(@"[A-Za-z ]{3,}", ErrorMessage = "La calle debe contener al menos 3 caracteres")]
        public string Calle { get; set; }

        [Required(ErrorMessage = "El campo es requerido")]
        [RegularExpression(@"[0-9]{1,4}", ErrorMessage = "La altura debe contener mínimo un número")]
        public int Altura { get; set; }

        [Required(ErrorMessage = "El campo es requerido")]
        [RegularExpression(@"[A-Za-z ]{4,}", ErrorMessage = "La provincia debe contener al menos 4 caracteres")]
        public string Provincia { get; set; }
        
        [Required(ErrorMessage = "El campo es requerido")]
        [Phone]
        [RegularExpression(@"11[0-9]{8}", ErrorMessage = "El teléfono es inválido")]
        public int Telefono { get; set; }

        [Required(ErrorMessage = "El campo es requerido")]
        [EmailAddress]
        [RegularExpression(@"^[^@]+@[^@]+\.[a-zA-Z]{2,}$", ErrorMessage = "El correo es inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo es requerido")]
        public string Tarjeta { get; set; }

        [Required(ErrorMessage = "El campo es requerido")]
        [CreditCard]
        [RegularExpression(@"5[1-5][0-9]{14}$", ErrorMessage = "La tarjeta es inválida")]
        public int NumeroTarjeta { get; set; }

        //Relaciones con otras entidades
       
        [ForeignKey(nameof(Libro))]
        public int LibroId { get; set; }
        public Libro Libro { get; set; }


    }
}
