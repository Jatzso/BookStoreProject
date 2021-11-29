using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreProject.Models
{
    public class Usuario
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[MaxLength(20, ErrorMessage = "La longitud máxima es {1}")]
		[MinLength(5, ErrorMessage = "La longitud mínima es {1}")]
		[Display(Name = "Usuario")]
		public string User { get; set; }

		[ScaffoldColumn(false)]
		public byte[] Contraseña { get; set; }

		[MaxLength(20, ErrorMessage = "La longitud máxima es {1}")]
		[MinLength(3, ErrorMessage = "La longitud mínima es {1}")]
		public string Nombre { get; set; }

		[Required]
		public Rol Rol { get; set; }

	}
}
