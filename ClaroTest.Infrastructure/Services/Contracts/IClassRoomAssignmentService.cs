using ClaroTest.Infrastructure.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaroTest.Infrastructure.Services.Contracts
{
    public interface IClassRoomAssignmentService
    {
        Task<List<ClassRoomAssignmentDto>> GetClassRoomAssignmentsAsync();
        Task<ClassRoomAssignmentDto> GetClassRoomAssignmentAsync(int classRoomAssignmentId);
        Task<ClassRoomAssignmentDto> DoClassRoomAssignmentAsync(ClassRoomAssignmentDto classRoomAssignment);
    }
}
