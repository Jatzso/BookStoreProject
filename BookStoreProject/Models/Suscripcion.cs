using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreProject.Models
{
    public class Suscripcion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [EmailAddress(ErrorMessage = "*Formato inválido")] 
        [Required(ErrorMessage = "*Campo requerido")]
        [RegularExpression(@"^[^@]+@[^@]+\.[a-zA-Z]{2,}$", ErrorMessage = "*Formato inválido")]
        public string Email { get; set; }

        //relaciones con otras entidades
        [ForeignKey(nameof(Usuario))]
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
