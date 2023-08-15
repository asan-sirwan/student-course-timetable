using Microsoft.AspNetCore.Mvc;
using student_course_timetable.DTOs.LecturerDTOs;
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
		public async Task<ActionResult<List<LecturerDTO>>> GetLecturers([FromQuery] bool detailed = false)
		{
			List<LecturerDTO> lecturers = await lecturerService.GetLecturers(detailed);
			return Ok(lecturers);
		}
	}
}