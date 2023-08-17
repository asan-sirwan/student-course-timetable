using Microsoft.EntityFrameworkCore;
using student_course_timetable.Data;
using student_course_timetable.Models;

namespace student_course_timetable.Repositories.AssignRepo
{
	public class AssignRepository : IAssignRepository
	{
		private readonly DataContext context;

		public AssignRepository(DataContext context)
		{
			this.context = context;
		}

		public async Task<bool> AssignStudentToCourse(int studentId, int courseId)
		{
			context.StudentCourses.Add(new StudentCourse
			{
				StudentId = studentId,
				CourseId = courseId
			});
			int saved = await context.SaveChangesAsync();

			return saved > 0;
		}

		public async Task<bool> DeleteAssignment(int studentId, int courseId)
		{
			StudentCourse? studentCourse = await context.StudentCourses
				.Where(sc => sc.StudentId == studentId && sc.CourseId == courseId)
				.FirstOrDefaultAsync();
			if (studentCourse == null)
			{ return false; }

			context.StudentCourses.Remove(studentCourse);
			int removed = await context.SaveChangesAsync();

			return removed > 0;
		}
	}
}