using ClaroTest.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaroTest.Domain.Contracts
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
    }
}
