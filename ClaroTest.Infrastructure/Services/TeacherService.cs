using ClaroTest.Domain.Contracts;
using ClaroTest.Domain.Models;
using ClaroTest.Infrastructure.Dtos;
using ClaroTest.Infrastructure.Services.Contracts;
using ClaroTest.Infrastructure.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaroTest.Infrastructure.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IClassRoomAssignmentRepository _classRoomAssignmentRepository;

        public TeacherService(ITeacherRepository teacherRepository, IClassRoomAssignmentRepository classRoomAssignmentRepository)
        {
            _teacherRepository = teacherRepository;
            _classRoomAssignmentRepository = classRoomAssignmentRepository;
        }

        public async Task<TeacherDto> GetTeacherAsync(int teacherId)
        {
            var teacher = await _teacherRepository.GetOneAsync(t => t.Id == teacherId);

            if (teacher != null)
                return MapDtos.MapTeacher(teacher);

            throw new Exception("Maestro no encontrado");
        }

        public async Task<List<TeacherDto>> GetTeachersAsync()
        {
            var teachers = await _teacherRepository.GetAllAsync();
            var teachersDto = teachers.Select(t => MapDtos.MapTeacher(t));

            return teachersDto.ToList();
        }

        public async Task RemoveTeacherAsync(int teacherId)
        {
            var classRoomAssignments = await _classRoomAssignmentRepository.GetAllWithFilterAsync(ca => ca.TeacherId == teacherId);

            _classRoomAssignmentRepository.RemoveClassRoomAssignments(classRoomAssignments.ToArray());

            var teacher = await _teacherRepository.GetOneAsync(t => t.Id == teacherId);

            if (teacher != null)
            {
                _teacherRepository.Remove(teacher);
                await _teacherRepository.SaveChangesAsync();
            }
        }

        public async Task<TeacherDto> SaveTeacherAsync(TeacherDto teacher)
        {
            var teacherEntity = new Teacher
            {
                FullName = teacher.FullName,
                Phone = teacher.Phone,
                Email = teacher.Email
            };

            var teacherInDb = await _teacherRepository.GetOneAsync(t => t.Phone.Equals(teacher.Phone) || t.Email.Equals(teacher.Email));

            if (teacherInDb != null) throw new Exception("El profesor con este teléfono o correo ya existe en la base de datos, por favor intente con uno nuevo");

            await _teacherRepository.AddAsync(teacherEntity);
            await _teacherRepository.SaveChangesAsync();

            teacher.Id = teacherEntity.Id;
            return teacher;
        }

        public async Task<TeacherDto> UpdateTeacherAsync(TeacherDto teacher)
        {
            var teacherEntity = await _teacherRepository.GetOneAsync(t => t.Id == teacher.Id);
            teacherEntity.Id = teacher.Id;
            teacherEntity.FullName = teacher.FullName;
            teacherEntity.Phone = teacher.Phone;
            teacherEntity.Email = teacher.Email;

            var teacherInDb = await _teacherRepository.GetOneAsync(t => t.Id != teacher.Id &&
            (t.Phone.Equals(teacher.Phone) || t.Email.Equals(teacher.Email)));

            if (teacherInDb != null) throw new Exception("El profesor con este teléfono o correo ya existe en la base de datos, por favor intente con uno nuevo");

            _teacherRepository.Update(teacherEntity);
            await _teacherRepository.SaveChangesAsync();

            return teacher;
        }
    }
}
