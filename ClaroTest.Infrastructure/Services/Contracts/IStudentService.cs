using ClaroTest.Infrastructure.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaroTest.Infrastructure.Services.Contracts
{
    public interface IStudentService
    {
        Task<List<StudentDto>> GetStudentsAsync();
        Task<StudentDto> GetStudentAsync(int studentId);
        Task<StudentDto> SaveStudentAsync(StudentDto student);
        Task<StudentDto> UpdateStudentAsync(StudentDto student);
        Task RemoveStudentAsync(int studentId);
    }
}
