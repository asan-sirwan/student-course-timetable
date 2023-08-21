using Microsoft.EntityFrameworkCore;
using student_course_timetable.Data;
using student_course_timetable.Models;

namespace student_course_timetable.Repositories.CourseRepo
{
	public class CourseRepository : ICourseRepository
	{
		private readonly DataContext context;

		public CourseRepository(DataContext context)
		{
			this.context = context;
		}

		public async Task<List<Course>> GetCourses()
		{
			List<Course> courses = await context.Courses
				.Include(c => c.Lecturer)
				.Include(c => c.Students)
				.ToListAsync();
			return courses;
		}

		public async Task<Course?> GetCourseById(int courseId)
		{
			Course? course = await context.Courses
				.Where(c => c.Id == courseId)
				.Include(c => c.Lecturer)
				.Include(c => c.Students)
				.FirstOrDefaultAsync();
			return course;
		}

		public async Task<bool> AddCourse(Course course)
		{
			context.Courses.Add(course);
			int saved = await context.SaveChangesAsync();

			return saved > 0;
		}

		public async Task<bool> UpdateCourse(Course course)
		{
			var trackedCourse = await context.Courses.FindAsync(course.Id);
			if (trackedCourse == null)
			{
				return false;
			}

			trackedCourse.Title = course.Title;
			trackedCourse.Description = course.Description;
			trackedCourse.CourseDateTime = course.CourseDateTime;
			trackedCourse.Lecturer = course.Lecturer;

			int updated = await context.SaveChangesAsync();
			return updated > 0;
		}

		public async Task<bool> DeleteCourse(Course course)
		{
			context.Courses.Remove(course);
			int removed = await context.SaveChangesAsync();

			return removed > 0;
		}
	}
}