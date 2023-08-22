using Microsoft.AspNetCore.Mvc;
using student_course_timetable.DTOs.StudentDTOs;
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
			try
			{
				if (!ModelState.IsValid)
				{
					BadRequest(ServiceResponse<StudentDTO>.Fail("Bad input", 400));
				}

				ServiceResponse<List<StudentDTO>> students = await studentService.GetStudents(detailed);
				if (!students.IsSuccess)
				{ return StatusCode(students.StatusCode, students); }

				return Ok(students.Data);
			}
			catch (Exception)
			{
				var error = ServiceResponse<StudentDTO>.Fail("Something went wrong :(", 500);
				return StatusCode(error.StatusCode, error);
			}
		}

		[HttpGet("with-courses")]
		public async Task<ActionResult<ServiceResponse<List<StudentDTO>>>> GetStudentsWithCourses([FromQuery] bool detailed = false)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					BadRequest(ServiceResponse<StudentDTO>.Fail("Bad input", 400));
				}

				ServiceResponse<List<StudentDTO>> students = await studentService.GetStudentsWithCourses(detailed);
				if (!students.IsSuccess)
				{ return StatusCode(students.StatusCode, students); }

				return Ok(students.Data);
			}
			catch (Exception)
			{
				var error = ServiceResponse<StudentDTO>.Fail("Something went wrong :(", 500);
				return StatusCode(error.StatusCode, error);
			}
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

			return CreatedAtAction(nameof(GetStudentById), new { id = student.Data!.Id }, student.Data);
		}

		[HttpPost("login")]
		public async Task<ActionResult<ServiceResponse<StudentDTO>>> StudentLogin([FromBody] StudentLoginDTO studentLoginDTO)
		{
			ServiceResponse<StudentDTO> student = await studentService.StudentLogin(studentLoginDTO);
			if (!student.IsSuccess)
			{ return StatusCode(student.StatusCode, student); }

			return Ok(student.Data);
		}

		[HttpPut]
		public async Task<ActionResult<ServiceResponse<StudentDTO>>> UpdateStudent([FromBody] StudentUpdateDTO updateStudent)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					BadRequest(ServiceResponse<StudentDTO>.Fail("Bad input", 400));
				}

				ServiceResponse<StudentDTO> student = await studentService.UpdateStudent(updateStudent);
				if (!student.IsSuccess)
				{ return StatusCode(student.StatusCode); }

				return Ok(student.Data);
			}
			catch (Exception)
			{
				var error = ServiceResponse<StudentDTO>.Fail("Something went wrong :(", 500);
				return StatusCode(error.StatusCode, error);
			}
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<ServiceResponse<StudentDTO>>> DeleteStudent(int id)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					BadRequest(ServiceResponse<StudentDTO>.Fail("Bad input", 400));
				}

				ServiceResponse<StudentDTO> student = await studentService.DeleteStudent(id);
				if (!student.IsSuccess)
				{ return StatusCode(student.StatusCode, student); }

				return Ok(student.Data);
			}
			catch (Exception)
			{
				var error = ServiceResponse<StudentDTO>.Fail("Something went wrong :(", 500);
				return StatusCode(error.StatusCode, error);
			}
		}
	}
}