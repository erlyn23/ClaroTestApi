using ClaroTest.Domain.Contracts;
using ClaroTest.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaroTest.Infrastructure.Implementations
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(ClaroTestDbContext dbContext) : base(dbContext)
        {

        }
    }
}
