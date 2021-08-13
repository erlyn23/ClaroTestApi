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
    public class ClassRoomAssignmentController : ControllerBase
    {
        private readonly IClassRoomAssignmentService _classRoomAssignmentService;

        public ClassRoomAssignmentController(IClassRoomAssignmentService classRoomAssignmentService)
        {
            _classRoomAssignmentService = classRoomAssignmentService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ClassRoomAssignmentDto>>> GetClassRoomAssignmentsAsync()
        {
            try
            {
                var classRoomAssginments = await _classRoomAssignmentService.GetClassRoomAssignmentsAsync();

                return Ok(classRoomAssginments);
            }
            catch(Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("{classRoomAssignmentId:int}", Name = "GetClassRoomAssignment")]
        public async Task<ActionResult<ClassRoomAssignmentDto>> GetClassRoomAssignmentAsync(int classRoomAssignmentId)
        {
            try
            {
                var classRoomAssignment = await _classRoomAssignmentService.GetClassRoomAssignmentAsync(classRoomAssignmentId);

                return Ok(classRoomAssignment);
            }
            catch(Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<ClassRoomAssignmentDto>> DoClassRoomAssignmentAsync(ClassRoomAssignmentDto classRoomAssignment)
        {
            try
            {
                var savedClassRoomAssignment = await _classRoomAssignmentService.DoClassRoomAssignmentAsync(classRoomAssignment);

                return CreatedAtRoute("GetClassRoomAssignment", new { classRoomAssignmentId = savedClassRoomAssignment.Id }, savedClassRoomAssignment);
            }
            catch(Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
