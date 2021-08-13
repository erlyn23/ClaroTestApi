using ClaroTest.Domain.Contracts;
using ClaroTest.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClaroTest.Infrastructure.Implementations
{
    public class ClassRoomAssignmentRepository : GenericRepository<ClassRoomAssignment>, IClassRoomAssignmentRepository
    {

        private readonly ClaroTestDbContext _dbContext;
        public ClassRoomAssignmentRepository(ClaroTestDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ClassRoomAssignment>> GetClassRoomAssignmentsWithRelationsAsync()
        {
            return await _dbContext.ClassRoomAssignments.Include(ca => ca.ClassRoom).Include(ca => ca.Student).Include(ca => ca.Teacher).Include(ca => ca.DayOfWeek).ToListAsync();
        }

        public async Task<ClassRoomAssignment> GetClassRoomAssignmentWithRelationsAsync(int classRoomAssginmentId)
        {
            return await _dbContext.ClassRoomAssignments.Where(ca => ca.Id == classRoomAssginmentId).Include(ca => ca.ClassRoom).Include(ca => ca.Student).Include(ca => ca.Teacher).Include(ca => ca.DayOfWeek).FirstOrDefaultAsync();
        }

        public async Task<int> GetClassRoomTeachersCountAsync(int classRoomId)
        {
            return await _dbContext.ClassRoomAssignments.Where(ca => ca.ClassRoomId == classRoomId).Select(ca => ca.TeacherId).Distinct().CountAsync();
        }

        public async Task<int> GetClassRoomStudentsCountAsync(int classRoomId)
        {
            return await _dbContext.ClassRoomAssignments.Where(ca => ca.ClassRoomId == classRoomId).Select(ca => ca.StudentId).Distinct().CountAsync();
        }

        public void RemoveClassRoomAssignments(ClassRoomAssignment[] classRoomAssignments)
        {
            _dbContext.ClassRoomAssignments.RemoveRange(classRoomAssignments);
        }
    }
}
