using ClaroTest.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaroTest.Infrastructure.Dtos
{
    [ValidateEnrollment]
    public class StudentDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre completo es obligatorio")]
        public string FullName { get; set; }
        [RegularExpression(@"^\d+$", ErrorMessage = "Solo puedes escribir números")]
        [Required(ErrorMessage = "El teléfono es obligatorio")]
        public string Phone { get; set; }

        [EmailAddress(ErrorMessage = "Debes escribir una dirección de correo válida")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La matrícula es obligatoria")]

        public string Enrollment { get; set; }
    }
    public class ValidateEnrollment : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dbContext = (ClaroTestDbContext)validationContext.GetService(typeof(ClaroTestDbContext));

            var studentDto = (StudentDto)value;

            if(studentDto != null)
            {
                var student = dbContext.Students.Where(s => s.Enrollment.Equals(studentDto.Enrollment) && s.Id != studentDto.Id).FirstOrDefault();
                
                if (student != null) return new ValidationResult("El estudiante con esta matrícula ya existe, intente con uno nuevo");
            }
            return ValidationResult.Success;
        }
    }
}
