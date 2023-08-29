using student_course_timetable.DTOs.CourseDTOs;

namespace student_course_timetable;

public class AttachmentCreateDTO
{
	public required string Title { get; set; }
	public required DateTime PostDate { get; set; }
	public required IFormFile File { get; set; }
	public required int CourseId { get; set; }
}
