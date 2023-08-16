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
			ServiceResponse<List<LecturerDTO>> lecturers = await lecturerService.GetLecturers(detailed);
			if (!lecturers.IsSuccess)
			{ return StatusCode(lecturers.StatusCode, lecturers); }

			return Ok(lecturers.Data);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<ServiceResponse<LecturerDTO>>> GetLecturerById(int id, [FromQuery] bool detailed = false)
		{
			ServiceResponse<LecturerDTO> lecturer = await lecturerService.GetLecturerById(id, detailed);
			if (!lecturer.IsSuccess)
			{ return StatusCode(lecturer.StatusCode, lecturer); }

			return Ok(lecturer.Data);
		}
	}
}