namespace student_course_timetable.DTOs.LecturerDTOs
{
	public class LecturerCreateDTO
	{
		public required string Name { get; set; }
		public required string Email { get; set; }
		public required string Password { get; set; }
		public required DateOnly BirthDate { get; set; }
		public required string Degree { get; set; }
	}
}