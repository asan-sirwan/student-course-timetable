using Microsoft.AspNetCore.Mvc;
using student_course_timetable.DTOs.StudentDTOs;
using student_course_timetable.Models;
using student_course_timetable.Services;
using student_course_timetable.Services.StudentService;

namespace student_course_timetable.Controllers
{
	[ApiController]
	[Route("/students")]
	public class StudentController : ControllerBase
	{
		private readonly IStudentService studentService;

		public StudentController(IStudentService studentService)
		{
			this.studentService = studentService;
		}

		[HttpGet]
		public async Task<ActionResult<ServiceResponse<List<StudentDTO>>>> GetStudents([FromQuery] bool detailed = false)
		{
			ServiceResponse<List<StudentDTO>> students = await studentService.GetStudents(detailed);
			if (!students.IsSuccess)
			{ return StatusCode(students.StatusCode, students); }

			return Ok(students.Data);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<ServiceResponse<StudentDTO>>> GetStudentById(int id, [FromQuery] bool detailed = false)
		{
			ServiceResponse<StudentDTO> student = await studentService.GetStudentById(id, detailed);
			if (!student.IsSuccess)
			{ return StatusCode(student.StatusCode, student); }

			return Ok(student.Data);
		}

		[HttpPost]
		public async Task<ActionResult<ServiceResponse<StudentDTO>>> AddStudent([FromBody] StudentCreateDTO newStudent)
		{
			ServiceResponse<StudentDTO> student = await studentService.AddStudent(newStudent);
			if (!student.IsSuccess)
			{ return StatusCode(student.StatusCode, student); }

			return CreatedAtAction(nameof(GetStudentById), new { id = student.Data!.StudentId }, student.Data);
		}
	}
}