using Microsoft.AspNetCore.Mvc;

namespace student_course_timetable.Controllers
{
    [ApiController]
    [Route("/")]
    public class RootController : ControllerBase
    {
        [HttpGet]
		public IActionResult Greet()
		{
			Dictionary<string, string> routes = new()
			{
				{"/lecturers", "Gets all lecturers"},
				{"/courses", "Gets all courses"},
				{"/students", "Gets all students"},
			};

			object[] response = {
				"Hello, here are the accessible routes:",
				routes,
				"You can add ?detailed=true to view the results in detail."
			};

			return Ok(response);
		}
    }
}