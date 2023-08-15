using System.ComponentModel.DataAnnotations;

namespace student_course_timetable.Models
{
    public class Course
    {
		[Key]
        public int CourseId { get; set; }

		[Required]
		[MaxLength(50)]
        public required string Title { get; set; }

		[Required]
		[MaxLength(255)]
        public required string Description { get; set; }

		[Required]
        public required DateTime CourseDateTime { get; set; }

		[Required]
		public required Lecturer Lecturer { get; set; }
		
		public List<Student> Students { get; } = new();
    }
}