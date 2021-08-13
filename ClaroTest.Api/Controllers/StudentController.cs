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
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<ActionResult<List<StudentDto>>> GetStudentsAsync()
        {
            try
            {
                var students = await _studentService.GetStudentsAsync();
                return Ok(students);
            }
            catch(Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("{studentId:int}", Name = "GetStudent")]
        public async Task<ActionResult<StudentDto>> GetStudentAsync(int studentId)
        {
            try
            {
                var student = await _studentService.GetStudentAsync(studentId);
                return Ok(student);
            }
            catch(Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<StudentDto>> SaveStudentAsync(StudentDto student)
        {
            try
            {
                var savedStudent = await _studentService.SaveStudentAsync(student);
                return CreatedAtRoute("GetStudent", new { studentId = savedStudent.Id }, savedStudent);
            }
            catch(Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut]
        public async Task<ActionResult<StudentDto>> UpdateStudentAsync(StudentDto student)
        {
            try
            {
                var updatedStudent = await _studentService.UpdateStudentAsync(student);
                return Ok(updatedStudent);
            }
            catch(Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpDelete("{studentId:int}")]
        public async Task<IActionResult> DeleteStudentAsync(int studentId)
        {
            try
            {
                await _studentService.RemoveStudentAsync(studentId);
                return Ok(new { Message = "Estudiante eliminado correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
