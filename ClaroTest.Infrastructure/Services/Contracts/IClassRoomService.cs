using ClaroTest.Infrastructure.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaroTest.Infrastructure.Services.Contracts
{
    public interface IClassRoomService
    {
        Task<List<ClassRoomDto>> GetClassRoomsAsync();
        Task<ClassRoomDto> GetClassRoomAsync(int classRoomId);
        Task<ClassRoomDto> SaveClassRoomAsync(ClassRoomDto classRoom);
        Task<ClassRoomDto> UpdateClassRoomAsync(ClassRoomDto classRoom);
        Task RemoveClassRoomAsync(int classRoomId);
    }
}
