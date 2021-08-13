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
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IClassRoomAssignmentRepository _classRoomAssignmentRepository;

        public StudentService(IStudentRepository studentRepository, IClassRoomAssignmentRepository classRoomAssignmentRepository)
        {
            _studentRepository = studentRepository;
            _classRoomAssignmentRepository = classRoomAssignmentRepository;
        }

        public async Task<StudentDto> GetStudentAsync(int studentId)
        {
            var student = await _studentRepository.GetOneAsync(s => s.Id == studentId);

            if (student != null)
                return MapDtos.MapStudent(student);

            throw new Exception("Estudiante no encontrado");
        }

        public async Task<List<StudentDto>> GetStudentsAsync()
        {
            var students = await _studentRepository.GetAllAsync();

            return students.Select(s => MapDtos.MapStudent(s)).ToList();
        }

        public async Task RemoveStudentAsync(int studentId)
        {
            var classRoomAssignments = await _classRoomAssignmentRepository.GetAllWithFilterAsync(ca => ca.StudentId == studentId);

            _classRoomAssignmentRepository.RemoveClassRoomAssignments(classRoomAssignments.ToArray());
            var student = await _studentRepository.GetOneAsync(s => s.Id == studentId);
            _studentRepository.Remove(student);
            await _studentRepository.SaveChangesAsync();
        }

        public async Task<StudentDto> SaveStudentAsync(StudentDto student)
        {
            var studentEntity = new Student
            {
                FullName = student.FullName,
                Phone = student.Phone,
                Email = student.Email,
                Enrollment = student.Enrollment
            };

            var studentInDb = await _studentRepository.GetOneAsync(s => s.Email.Equals(student.Email) || s.Phone.Equals(student.Phone));

            if (studentInDb != null) throw new Exception("El estudiante con este teléfono o correo ya existe en la base de datos, por favor, intenta con uno nuevo");

            await _studentRepository.AddAsync(studentEntity);
            await _studentRepository.SaveChangesAsync();

            student.Id = studentEntity.Id;
            
            return student;
        }

        public async Task<StudentDto> UpdateStudentAsync(StudentDto student)
        {
            var studentEntity = await _studentRepository.GetOneAsync(s => s.Id == student.Id);

            studentEntity.FullName = student.FullName;
            studentEntity.Email = student.Email;
            studentEntity.Phone = student.Phone;
            studentEntity.Enrollment = student.Enrollment;

            var studentInDb = await _studentRepository.GetOneAsync(s => s.Id != student.Id && (s.Email.Equals(student.Email) || s.Phone.Equals(student.Phone)));

            if (studentInDb != null) throw new Exception("El estudiante con este teléfono o correo ya existe en la base de datos, por favor, intenta con uno nuevo");

            _studentRepository.Update(studentEntity);
            await _studentRepository.SaveChangesAsync();
            
            return student;
        }
    }
}
