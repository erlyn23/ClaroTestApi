using ClaroTest.Domain.Contracts;
using ClaroTest.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaroTest.Infrastructure.Implementations
{
    public class TeacherRepository : GenericRepository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(ClaroTestDbContext dbContext) : base(dbContext)
        {

        }
    }
}
