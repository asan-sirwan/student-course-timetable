namespace student_course_timetable.DTOs.CourseDTOs
{
    public class CourseUpdateDTO
    {
        public required int CourseId { get; set; }
        public required string Title { get; set; }
		public required string Description { get; set; }
		public required DateTime CourseDateTime { get; set; }
		public required int LecturerId { get; set; }
    }
}