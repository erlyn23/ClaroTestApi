using System;
using System.Collections.Generic;

#nullable disable

namespace ClaroTest.Domain.Models
{
    public partial class Teacher
    {
        public Teacher()
        {
            ClassRoomAssignments = new HashSet<ClassRoomAssignment>();
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public virtual ICollection<ClassRoomAssignment> ClassRoomAssignments { get; set; }
    }
}
