using Microsoft.AspNetCore.Mvc;
using student_course_timetable.DTOs.LecturerDTOs;
using student_course_timetable.Services;
using student_course_timetable.Services.LecturerService;

namespace student_course_timetable.Controllers
{
	[ApiController]
	[Route("/lecturers")]
	public class LecturerController : ControllerBase
	{
		private readonly ILecturerService lecturerService;

		public LecturerController(ILecturerService lecturerService)
		{
			this.lecturerService = lecturerService;
		}

		[HttpGet]
		public async Task<ActionResult<ServiceResponse<List<LecturerDTO>>>> GetLecturers([FromQuery] bool detailed = false)
		{
			if (!ModelState.IsValid)
			{
				BadRequest(ServiceResponse<LecturerDTO>.Fail("Bad input", 400));
			}

			ServiceResponse<List<LecturerDTO>> lecturers = await lecturerService.GetLecturers(detailed);
			if (!lecturers.IsSuccess)
			{ return StatusCode(lecturers.StatusCode, lecturers); }

			return Ok(lecturers.Data);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<ServiceResponse<LecturerDTO>>> GetLecturerById(int id, [FromQuery] bool detailed = false)
		{
			if (!ModelState.IsValid)
			{
				BadRequest(ServiceResponse<LecturerDTO>.Fail("Bad input", 400));
			}

			ServiceResponse<LecturerDTO> lecturer = await lecturerService.GetLecturerById(id, detailed);
			if (!lecturer.IsSuccess)
			{ return StatusCode(lecturer.StatusCode, lecturer); }

			return Ok(lecturer.Data);
		}

		[HttpPost]
		public async Task<ActionResult<ServiceResponse<LecturerDTO>>> AddLecturer([FromBody] LecturerCreateDTO newLecturer)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					BadRequest(ServiceResponse<LecturerDTO>.Fail("Bad input", 400));
				}

				ServiceResponse<LecturerDTO> lecturer = await lecturerService.AddLecturer(newLecturer);
				if (!lecturer.IsSuccess)
				{ return StatusCode(lecturer.StatusCode, lecturer); }

				return CreatedAtAction(nameof(GetLecturerById), new { id = lecturer.Data!.Id }, lecturer.Data);
			}
			catch (Exception)
			{
				var error = ServiceResponse<LecturerDTO>.Fail("Something went wrong :(", 500);
				return StatusCode(error.StatusCode, error);
			}
		}

		[HttpPut]
		public async Task<ActionResult<ServiceResponse<LecturerDTO>>> UpdateLecturer([FromBody] LecturerUpdateDTO updateLecturer)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					BadRequest(ServiceResponse<LecturerDTO>.Fail("Bad input", 400));
				}

				ServiceResponse<LecturerDTO> lecturer = await lecturerService.UpdateLecturer(updateLecturer);
				if (!lecturer.IsSuccess)
				{ return StatusCode(lecturer.StatusCode); }

				return Ok(lecturer.Data);
			}
			catch (Exception)
			{
				var error = ServiceResponse<LecturerDTO>.Fail("Something went wrong :(", 500);
				return StatusCode(error.StatusCode, error);
			}
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<ServiceResponse<LecturerDTO>>> DeleteLecturer(int id)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					BadRequest(ServiceResponse<LecturerDTO>.Fail("Bad input", 400));
				}

				ServiceResponse<LecturerDTO> lecturer = await lecturerService.DeleteLecturer(id);
				if (!lecturer.IsSuccess)
				{ return StatusCode(lecturer.StatusCode, lecturer); }

				return Ok(lecturer.Data);
			}
			catch (Exception)
			{
				var error = ServiceResponse<LecturerDTO>.Fail("Something went wrong :(", 500);
				return StatusCode(error.StatusCode, error);
			}
		}
	}
}