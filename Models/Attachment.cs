using System.ComponentModel.DataAnnotations;
using student_course_timetable.Models;

namespace student_course_timetable;

public class Attachment
{
	[Key]
	public int Id { get; set; }

	[Required]
	[MaxLength(50)]
	public required string Title { get; set; }

	[Required]
	public required DateTime PostDate { get; set; }

	[Required]
	public required IFormFile File { get; set; }

	[Required]	
	public required Course Course { get; set; }
}
