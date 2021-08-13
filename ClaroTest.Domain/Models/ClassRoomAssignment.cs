using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace ClaroTest.Domain.Models
{
    public partial class ClassRoomAssignment
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public int StudentId { get; set; }
        public int ClassRoomId { get; set; }
        public int DayOfWeekId { get; set; }
        public DateTime StartHour { get; set; }
        public DateTime EndHour { get; set; }

        public virtual ClassRoom ClassRoom { get; set; }
        public virtual DaysOfWeek DayOfWeek { get; set; }
        public virtual Student Student { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}
