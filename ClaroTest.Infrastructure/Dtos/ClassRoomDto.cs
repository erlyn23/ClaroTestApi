using ClaroTest.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaroTest.Infrastructure.Dtos
{
    [CodeValidate]
    public class ClassRoomDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre del curso es obligatorio")]
        public string Name { get; set; }
        [Required(ErrorMessage = "El código del curso es obligatorio")]
        public string Code { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Debes especificar la cantidad máxima de maestros")]
        public int TeachersQuantity { get; set; }
        [Required(ErrorMessage = "Debes especificar la cantidad máxima de estudiantes")]
        public int StudentsQuantity { get; set; }
    }

    public class CodeValidate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dbContext = (ClaroTestDbContext)validationContext.GetService(typeof(ClaroTestDbContext));

            var classRoomDto = (ClassRoomDto)value;

            if(classRoomDto != null)
            {
                var classRoom = dbContext.ClassRooms.Where(c => c.Code.Equals(classRoomDto.Code) && c.Id != classRoomDto.Id).FirstOrDefault();

                if (classRoom != null) return new ValidationResult("El curso con este código ya ha sido creado, intente con un código diferente");
            }

            return ValidationResult.Success;
        }
    }
}
