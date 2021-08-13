using ClaroTest.Infrastructure.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaroTest.Infrastructure.Services.Contracts
{
    public interface ITeacherService
    {
        Task<List<TeacherDto>> GetTeachersAsync();
        Task<TeacherDto> GetTeacherAsync(int teacherId);
        Task<TeacherDto> SaveTeacherAsync(TeacherDto teacher);
        Task<TeacherDto> UpdateTeacherAsync(TeacherDto teacher);
        Task RemoveTeacherAsync(int teacherId);
    }
}
