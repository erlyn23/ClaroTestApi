using ClaroTest.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaroTest.Infrastructure.Dtos
{
    public class ClassRoomAssignmentDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El maestro es obligatorio")]
        public int TeacherId { get; set; }
        [Required(ErrorMessage = "El estudiante es obligatorio")]
        public int StudentId { get; set; }
        [Required(ErrorMessage = "El curso es obligatorio")]
        public int ClassRoomId { get; set; }
        [Required(ErrorMessage = "El día es obligatorio")]
        public int DayOfWeekId { get; set; }
        public string DayOfWeekName { get; set; }
        [Required(ErrorMessage = "La hora de inicio es obligatoria")]
        public string StartHour { get; set; }
        [Required(ErrorMessage = "La hora de final es obligatoria")]
        public string EndHour { get; set; }

        public StudentDto Student { get; set; }
        public TeacherDto Teacher { get; set; }
        public ClassRoomDto ClassRoom { get; set; }
    }
}
