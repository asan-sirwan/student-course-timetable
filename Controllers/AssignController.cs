using Microsoft.AspNetCore.Mvc;
using student_course_timetable.Models;
using student_course_timetable.Services;
using student_course_timetable.Services.AssignService;

namespace student_course_timetable.Controllers
{
	[ApiController]
	[Route("/assign")]
	public class AssignController : ControllerBase
	{
		private readonly IAssignService assignService;

		public AssignController(IAssignService assignService)
		{
			this.assignService = assignService;
		}

		[HttpPost]
		public async Task<ActionResult<ServiceResponse<bool>>> AssignStudentToCourse([FromBody] StudentCourse studentCourse)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					BadRequest(ServiceResponse<bool>.Fail("Bad input", 400));
				}

				ServiceResponse<bool> assignment = await assignService.AssignStudentToCourse(studentCourse);
				if (!assignment.IsSuccess)
				{ return StatusCode(assignment.StatusCode, assignment); }

				return StatusCode(assignment.StatusCode, "Course assigned to student successfully.");
			}
			catch (Exception)
			{
				var error = ServiceResponse<bool>.Fail("Something went wrong :(", 500);
				return StatusCode(error.StatusCode, error);
			}
		}

		[HttpDelete("{studentId}/{courseId}")]
		public async Task<ActionResult<ServiceResponse<bool>>> DeleteLecturer(int studentId, int courseId)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					BadRequest(ServiceResponse<bool>.Fail("Bad input", 400));
				}

				ServiceResponse<bool> assignment = await assignService.DeleteAssignment(studentId, courseId);
				if (!assignment.IsSuccess)
				{ return StatusCode(assignment.StatusCode, assignment); }

				return StatusCode(assignment.StatusCode, "Course assignment deleted succesfully");
			}
			catch (Exception)
			{
				var error = ServiceResponse<bool>.Fail("Something went wrong :(", 500);
				return StatusCode(error.StatusCode, error);
			}
		}
	}
}