using System.Text.Json.Serialization;
using student_course_timetable.DTOs.CourseDTOs;

namespace student_course_timetable.DTOs.LecturerDTOs
{
	public class LecturerDTO
	{
		public int Id { get; set; }
		public required string Name { get; set; }
		public required string Email { get; set; }
		public required DateOnly BirthDate { get; set; }
		public required string Degree { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public List<CourseDTO>? Courses { get; set; }
	}
}