using System.Text.Json.Serialization;
using student_course_timetable.DTOs.LecturerDTOs;
using student_course_timetable.DTOs.StudentDTOs;

namespace student_course_timetable.DTOs.CourseDTOs
{
    public class CourseDTO
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required DateTime CourseDateTime { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public LecturerDTO? Lecturer { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public List<StudentDTO>? Students { get; set; }
    }
}