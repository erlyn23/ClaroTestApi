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
    public class ClassRoomService : IClassRoomService
    {
        private readonly IClassRoomRepository _classRoomRepository;
        private readonly IClassRoomAssignmentRepository _classRoomAssignmentRepository;

        public ClassRoomService(IClassRoomRepository classRoomRepository, IClassRoomAssignmentRepository classRoomAssignmentRepository)
        {
            _classRoomRepository = classRoomRepository;
            _classRoomAssignmentRepository = classRoomAssignmentRepository;
        }
        public async Task<ClassRoomDto> GetClassRoomAsync(int classRoomId)
        {
            var classRoom = await _classRoomRepository.GetOneAsync(c => c.Id == classRoomId);

            if (classRoom != null)
                return MapDtos.MapClassRoom(classRoom);

            throw new Exception("Curso no encontrado");
        }

        public async Task<List<ClassRoomDto>> GetClassRoomsAsync()
        {
            var classRooms = await _classRoomRepository.GetAllAsync();

            return classRooms.Select(c => MapDtos.MapClassRoom(c)).ToList();
        }

        public async Task RemoveClassRoomAsync(int classRoomId)
        {
            var classRoomAssignments = await _classRoomAssignmentRepository.GetAllWithFilterAsync(ca => ca.ClassRoomId == classRoomId);

            _classRoomAssignmentRepository.RemoveClassRoomAssignments(classRoomAssignments.ToArray());

            var classRoom = await _classRoomRepository.GetOneAsync(c => c.Id == classRoomId);

            _classRoomRepository.Remove(classRoom);
            await _classRoomRepository.SaveChangesAsync();
        }

        public async Task<ClassRoomDto> SaveClassRoomAsync(ClassRoomDto classRoom)
        {
            var classRoomEntity = new ClassRoom
            {
                Name = classRoom.Name,
                Code = classRoom.Code,
                Description = classRoom.Description,
                StudentsQuantity = classRoom.StudentsQuantity,
                TeachersQuantity = classRoom.TeachersQuantity
            };

            await _classRoomRepository.AddAsync(classRoomEntity);
            await _classRoomRepository.SaveChangesAsync();

            classRoom.Id = classRoomEntity.Id;

            return classRoom;
        }

        public async Task<ClassRoomDto> UpdateClassRoomAsync(ClassRoomDto classRoom)
        {
            var classRoomEntity = await _classRoomRepository.GetOneAsync(c => c.Id == classRoom.Id);

            classRoomEntity.Name = classRoom.Name;
            classRoomEntity.Code = classRoom.Code;
            classRoomEntity.Description = classRoom.Description;
            classRoomEntity.StudentsQuantity = classRoom.StudentsQuantity;
            classRoomEntity.TeachersQuantity = classRoom.TeachersQuantity;

            _classRoomRepository.Update(classRoomEntity);
            await _classRoomRepository.SaveChangesAsync();

            return classRoom;
        }
    }
}
