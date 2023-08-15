using System.Text.Json.Serialization;
using student_course_timetable.Models;

namespace student_course_timetable.DTOs.CourseDTOs
{
    public class CourseDTO
    {
        public int CourseId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required DateTime CourseDateTime { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public Lecturer? Lecturer { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public List<Student>? Students { get; set; }
    }
}