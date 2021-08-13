using ClaroTest.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaroTest.Infrastructure.Dtos
{
    public class TeacherDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string FullName { get; set; }
        [RegularExpression(@"^\d+$", ErrorMessage = "Solo puedes escribir números")]
        [Required(ErrorMessage = "El número de teléfono es obligatorio")]
        public string Phone { get; set; }

        [EmailAddress(ErrorMessage = "Ingrese una dirección de correo válida")]
        public string Email { get; set; }
    }
}
