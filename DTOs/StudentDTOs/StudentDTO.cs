using System.Text.Json.Serialization;
using student_course_timetable.DTOs.CourseDTOs;

namespace student_course_timetable.DTOs.StudentDTOs
{
    public class StudentDTO
    {
        public int StudentId { get; set; }
		public required string Name { get; set; }
		public required string Email { get; set; }
		public required DateOnly BirthDate { get; set; }
		public required string Address { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public List<CourseDTO>? Courses { get; set; }
    }
}