using ClaroTest.Infrastructure.Dtos;
using ClaroTest.Infrastructure.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClaroTest.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TeacherDto>>> GetTeachersAsync()
        {
            try
            {
                var teachers = await _teacherService.GetTeachersAsync();
                return Ok(teachers);
            }
            catch(Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("{teacherId:int}", Name = "GetTeacher")]
        public async Task<ActionResult<TeacherDto>> GetTeacherAsync(int teacherId)
        {
            try
            {
                var teacher = await _teacherService.GetTeacherAsync(teacherId);
                return Ok(teacher);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<TeacherDto>> AddTeacherAsync(TeacherDto teacher)
        {
            try
            {
                var savedTeacher = await _teacherService.SaveTeacherAsync(teacher);
                return CreatedAtRoute("GetTeacher", new { teacherId = savedTeacher.Id }, savedTeacher);
            }
            catch(Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut]
        public async Task<ActionResult<TeacherDto>> UpdateTeacherAsync(TeacherDto teacher)
        {
            try
            {
                var updatedTeacher = await _teacherService.UpdateTeacherAsync(teacher);
                return Ok(updatedTeacher);
            }
            catch(Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("{teacherId:int}")]
        public async Task<IActionResult> RemoveTeacherAsync(int teacherId)
        {
            try
            {
                await _teacherService.RemoveTeacherAsync(teacherId);
                return Ok(new { Message = "Maestro eliminado correctamente"});
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
