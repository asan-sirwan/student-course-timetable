using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace student_course_timetable.Models
{
	[Index(nameof(Email), IsUnique = true)]
    public class Student
    {
		[Key]
        public int StudentId { get; set; }

		[Required]
		[MaxLength(50)]
		public required string Name { get; set; }

		[Required]
		[MaxLength(255)]
		public required string Email { get; set; }

		[Required]
		[Column(TypeName = "char")]
		[MaxLength(60)]
		public required string Password { get; set; }

		[Required]
		public required DateOnly BirthDate { get; set; }

		[Required]
		[MaxLength(255)]
		public required string Address { get; set; }

		public List<Course> Courses { get; } = new();
    }
}