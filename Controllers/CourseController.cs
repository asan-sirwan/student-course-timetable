using Microsoft.AspNetCore.Mvc;
using student_course_timetable.DTOs.CourseDTOs;
using student_course_timetable.Services;
using student_course_timetable.Services.CourseService;

namespace student_course_timetable.Controllers
{
	[ApiController]
	[Route("/courses")]
	public class CourseController : ControllerBase
	{
		private readonly ICourseService courseService;

		public CourseController(ICourseService courseService)
		{
			this.courseService = courseService;
		}

		[HttpGet]
		public async Task<ActionResult<ServiceResponse<List<CourseDTO>>>> GetCourses([FromQuery] bool detailed = false)
		{
			ServiceResponse<List<CourseDTO>> courses = await courseService.GetCourses(detailed);
			if (!courses.IsSuccess)
			{ return StatusCode(courses.StatusCode, courses); }

			return Ok(courses.Data);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<ServiceResponse<CourseDTO>>> GetCourseById(int id, [FromQuery] bool detailed = false)
		{
			ServiceResponse<CourseDTO> course = await courseService.GetCourseById(id, detailed);
			if (!course.IsSuccess)
			{ return StatusCode(course.StatusCode, course); }

			return Ok(course.Data);
		}

		[HttpPost]
		public async Task<ActionResult<ServiceResponse<CourseDTO>>> AddCourse([FromBody] CourseCreateDTO newCourse)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					BadRequest(ServiceResponse<CourseDTO>.Fail("Bad input", 400));
				}

				ServiceResponse<CourseDTO> course = await courseService.AddCourse(newCourse);
				if (!course.IsSuccess)
				{ return StatusCode(course.StatusCode, course); }

				return CreatedAtAction(nameof(GetCourseById), new { id = course.Data!.Id }, course.Data);
			}
			catch (Exception)
			{
				var error = ServiceResponse<CourseDTO>.Fail("Something went wrong :(", 500);
				return StatusCode(error.StatusCode, error);
			}
		}

		[HttpPut]
		public async Task<ActionResult<ServiceResponse<CourseDTO>>> UpdateCourse([FromBody] CourseUpdateDTO updateCourse)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					BadRequest(ServiceResponse<CourseDTO>.Fail("Bad input", 400));
				}

				ServiceResponse<CourseDTO> course = await courseService.UpdateCourse(updateCourse);
				if (!course.IsSuccess)
				{ return StatusCode(course.StatusCode, course); }

				return Ok(course.Data);
			}
			catch (Exception)
			{
				var error = ServiceResponse<CourseDTO>.Fail("Something went wrong :(", 500);
				return StatusCode(error.StatusCode, error);
			}
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<ServiceResponse<CourseDTO>>> DeleteCourse(int id)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					BadRequest(ServiceResponse<CourseDTO>.Fail("Bad input", 400));
				}

				ServiceResponse<CourseDTO> course = await courseService.DeleteCourse(id);
				if (!course.IsSuccess)
				{ return StatusCode(course.StatusCode, course); }

				return Ok(course.Data);
			}
			catch (Exception)
			{
				var error = ServiceResponse<CourseDTO>.Fail("Something went wrong :(", 500);
				return StatusCode(error.StatusCode, error);
			}
		}
	}
}