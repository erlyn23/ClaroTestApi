using System;
using System.Collections.Generic;

#nullable disable

namespace ClaroTest.Domain.Models
{
    public partial class DaysOfWeek
    {
        public DaysOfWeek()
        {
            ClassRoomAssignments = new HashSet<ClassRoomAssignment>();
        }

        public int Id { get; set; }
        public string Day { get; set; }

        public virtual ICollection<ClassRoomAssignment> ClassRoomAssignments { get; set; }
    }
}
