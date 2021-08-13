using ClaroTest.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClaroTest.Domain.Contracts
{
    public interface IClassRoomAssignmentRepository : IGenericRepository<ClassRoomAssignment>
    {
        Task<List<ClassRoomAssignment>> GetClassRoomAssignmentsWithRelationsAsync();
        Task<ClassRoomAssignment> GetClassRoomAssignmentWithRelationsAsync(int classRoomAssignmentId);
        Task<int> GetClassRoomTeachersCountAsync(int classRoomId);
        Task<int> GetClassRoomStudentsCountAsync(int classRoomId);
        void RemoveClassRoomAssignments(ClassRoomAssignment[] classRoomAssignments);
    }
}
