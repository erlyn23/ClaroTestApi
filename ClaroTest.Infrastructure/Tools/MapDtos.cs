using ClaroTest.Domain.Models;
using ClaroTest.Infrastructure.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaroTest.Infrastructure.Tools
{
    public static class MapDtos
    {
        public static TeacherDto MapTeacher(Teacher teacher)
        {
            return new TeacherDto
            {
                Id = teacher.Id,
                FullName = teacher.FullName,
                Phone = teacher.Phone,
                Email = teacher.Email
            };
        } 

        public static StudentDto MapStudent(Student student)
        {
            return new StudentDto
            {
                Id = student.Id,
                FullName = student.FullName,
                Email = student.Email,
                Phone = student.Phone,
                Enrollment = student.Enrollment
            };
        }

        public static ClassRoomDto MapClassRoom(ClassRoom classRoom)
        {
            return new ClassRoomDto
            {
                Id = classRoom.Id,
                Name = classRoom.Name,
                Code = classRoom.Code,
                Description = classRoom.Description,
                TeachersQuantity = classRoom.TeachersQuantity,
                StudentsQuantity = classRoom.StudentsQuantity
            };
        }

        public static ClassRoomAssignmentDto MapClassRoomAssignment(ClassRoomAssignment classRoomAssignment)
        {
            return new ClassRoomAssignmentDto
            {
                Id = classRoomAssignment.Id,
                TeacherId = classRoomAssignment.TeacherId,
                StudentId = classRoomAssignment.StudentId,
                ClassRoomId = classRoomAssignment.ClassRoomId,
                DayOfWeekId = classRoomAssignment.DayOfWeekId,
                DayOfWeekName = classRoomAssignment.DayOfWeek.Day,
                StartHour = classRoomAssignment.StartHour.ToString("H:mm"),
                EndHour = classRoomAssignment.EndHour.ToString("H:mm"),
                Teacher = MapTeacher(classRoomAssignment.Teacher),
                Student = MapStudent(classRoomAssignment.Student),
                ClassRoom = MapClassRoom(classRoomAssignment.ClassRoom)
            };
        } 
    }
}
