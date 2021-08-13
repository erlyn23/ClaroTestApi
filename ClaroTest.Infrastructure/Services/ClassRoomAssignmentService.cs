using ClaroTest.Domain.Contracts;
using ClaroTest.Domain.Models;
using ClaroTest.Infrastructure.Dtos;
using ClaroTest.Infrastructure.Implementations;
using ClaroTest.Infrastructure.Services.Contracts;
using ClaroTest.Infrastructure.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ClaroTest.Infrastructure.Services
{
    public class ClassRoomAssignmentService : IClassRoomAssignmentService
    {
        private readonly IClassRoomAssignmentRepository _classRoomAssignmentRepository;
        private readonly IClassRoomRepository _classRoomRepository;

        public ClassRoomAssignmentService(IClassRoomAssignmentRepository classRoomAssignmentRepository, IClassRoomRepository classRoomRepository)
        {
            _classRoomAssignmentRepository = classRoomAssignmentRepository;
            _classRoomRepository = classRoomRepository;
        }
        public async Task<ClassRoomAssignmentDto> DoClassRoomAssignmentAsync(ClassRoomAssignmentDto classRoomAssignment)
        {
            var classRoom = await _classRoomRepository.GetOneAsync(c => c.Id == classRoomAssignment.ClassRoomId);

            var classRoomAssignmentsByTeacher = await _classRoomAssignmentRepository.GetOneAsync(ca => ca.ClassRoomId == classRoomAssignment.ClassRoomId && ca.TeacherId == classRoomAssignment.TeacherId);

            int actualTeachersQuantity = 0;

            if(classRoomAssignmentsByTeacher == null)
            {
                actualTeachersQuantity = await _classRoomAssignmentRepository.GetClassRoomTeachersCountAsync(classRoomAssignment.ClassRoomId);
            }

            bool isStudentInClassRoom = await IsStudentInClassRoom(classRoomAssignment.StudentId, classRoomAssignment.ClassRoomId, classRoomAssignment.DayOfWeekId);
            
            int actualStudentsQantity = await _classRoomAssignmentRepository.GetClassRoomStudentsCountAsync(classRoomAssignment.ClassRoomId);
            

            var startHour = TimeSpan.Parse(classRoomAssignment.StartHour);
            var endHour = TimeSpan.Parse(classRoomAssignment.EndHour);

            if (actualTeachersQuantity > 0 && classRoom.TeachersQuantity <
                actualTeachersQuantity + 1)
                throw new Exception("Ya este curso alcanzó el límite de maestros");
            else if (isStudentInClassRoom)
                throw new Exception("Este estudiante ya fue asignado a este curso este día, por favor, seleccione un curso diferente o un día diferente");
            else if (classRoom.StudentsQuantity < actualStudentsQantity + 1)
                throw new Exception("Ya este curso alcanzó el límite de estudiantes");
            else if (startHour < new TimeSpan(8, 0, 0) || endHour > new TimeSpan(17, 0, 0) )
                throw new Exception("La clase está fuera del horario de 8 horas");

            var classRoomAssignmentEntity = new ClassRoomAssignment
            {
                TeacherId = classRoomAssignment.TeacherId,
                StudentId = classRoomAssignment.StudentId,
                ClassRoomId = classRoomAssignment.ClassRoomId,
                DayOfWeekId = classRoomAssignment.DayOfWeekId,
                StartHour = DateTime.Parse(startHour.ToString()),
                EndHour = DateTime.Parse(endHour.ToString())
            };

            await _classRoomAssignmentRepository.AddAsync(classRoomAssignmentEntity);
            await _classRoomAssignmentRepository.SaveChangesAsync();

            classRoomAssignment.Id = classRoomAssignmentEntity.Id;
            return classRoomAssignment;
        }

        private async Task<bool> IsStudentInClassRoom(int studentId, int classRoomId, int dayOfWeekId)
        {
            var studentInClassRoom = await _classRoomAssignmentRepository.GetOneAsync(ca => ca.StudentId == studentId && ca.ClassRoomId == classRoomId && ca.DayOfWeekId == dayOfWeekId);

            return (studentInClassRoom != null) ? true : false;
        }

        public async Task<ClassRoomAssignmentDto> GetClassRoomAssignmentAsync(int classRoomAssignmentId)
        {
            var classRoomAssignment = await _classRoomAssignmentRepository.GetClassRoomAssignmentWithRelationsAsync(classRoomAssignmentId);

            if(classRoomAssignment != null) return MapDtos.MapClassRoomAssignment(classRoomAssignment);

            throw new Exception("Asignación no encontrada");
        }

        public async Task<List<ClassRoomAssignmentDto>> GetClassRoomAssignmentsAsync()
        {
            var classRoomAssginments = await _classRoomAssignmentRepository.GetClassRoomAssignmentsWithRelationsAsync();

            return classRoomAssginments.Select(ca => MapDtos.MapClassRoomAssignment(ca)).ToList();
        }
    }
}
