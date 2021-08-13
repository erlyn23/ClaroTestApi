using System;
using System.Collections.Generic;

#nullable disable

namespace ClaroTest.Domain.Models
{
    public partial class ClassRoom
    {
        public ClassRoom()
        {
            ClassRoomAssignments = new HashSet<ClassRoomAssignment>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int StudentsQuantity { get; set; }
        public int TeachersQuantity { get; set; }

        public virtual ICollection<ClassRoomAssignment> ClassRoomAssignments { get; set; }
    }
}
