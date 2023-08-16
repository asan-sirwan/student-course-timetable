namespace student_course_timetable.DTOs.StudentDTOs
{
    public class StudentUpdateDTO
    {
        public required int StudentId { get; set; }
        public required string Name { get; set; }
		public required string Email { get; set; }
		public required string Password { get; set; }
		public required DateOnly BirthDate { get; set; }
		public required string Address { get; set; }
    }
}