using ClaroTest.Infrastructure.Dtos;
using ClaroTest.Infrastructure.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClaroTest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassRoomController : ControllerBase
    {
        private readonly IClassRoomService _classRoomService;

        public ClassRoomController(IClassRoomService classRoomService)
        {
            _classRoomService = classRoomService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ClassRoomDto>>> GetClassRoomsAsync()
        {
            try
            {
                var classRooms = await _classRoomService.GetClassRoomsAsync();
                return Ok(classRooms);
            }
            catch(Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("{classRoomId:int}", Name = "GetClassRoom")]
        public async Task<ActionResult<ClassRoomDto>> GetClassRoomAsync(int classRoomId)
        {
            try
            {
                var classRoom = await _classRoomService.GetClassRoomAsync(classRoomId);
                return Ok(classRoom);
            }
            catch(Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<ClassRoomDto>> SaveClassRoomAsync(ClassRoomDto classRoomDto)
        {
            try
            {
                var savedClassRoom = await _classRoomService.SaveClassRoomAsync(classRoomDto);
                return CreatedAtRoute("GetClassRoom", new { classRoomId = savedClassRoom.Id }, savedClassRoom);
            }
            catch(Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut]
        public async Task<ActionResult<ClassRoomDto>> UpdateClassRoomAsync(ClassRoomDto classRoomDto)
        {
            try
            {
                var updatedClassRoom = await _classRoomService.UpdateClassRoomAsync(classRoomDto);
                return Ok(updatedClassRoom);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("{classRoomId:int}")]
        public async Task<IActionResult> DeleteClassRoomAsync(int classRoomId)
        {
            try
            {
                await _classRoomService.RemoveClassRoomAsync(classRoomId);
                return Ok(new { Message = "Curso eliminado correctamente"});
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
