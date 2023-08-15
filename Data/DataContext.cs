using Microsoft.EntityFrameworkCore;
using student_course_timetable.Models;

namespace student_course_timetable.Data
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options)
		{ }

		public DbSet<Student> Students { get; set; }
		public DbSet<Lecturer> Lecturers { get; set; }
		public DbSet<Course> Courses { get; set; }
		public DbSet<StudentCourse> StudentCourses { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Lecturer>()
				.HasMany(e => e.Courses)
				.WithOne(e => e.Lecturer)
				.HasForeignKey("LecturerId")
				.IsRequired();

			modelBuilder.Entity<Student>()
				.HasMany(e => e.Courses)
				.WithMany(e => e.Students)
				.UsingEntity<StudentCourse>();
		}
	}
}