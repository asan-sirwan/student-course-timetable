using Microsoft.EntityFrameworkCore;
using student_course_timetable.Data;
using student_course_timetable.Models;

namespace student_course_timetable.Repositories.StudentRepo
{
	public class StudentRepository : IStudentRepository
	{
		private readonly DataContext context;

		public StudentRepository(DataContext context)
		{
			this.context = context;
		}

		public async Task<List<Student>> GetStudents()
		{
			List<Student> students = await context.Students
				.Include(s => s.Courses)
				.ThenInclude(c => c.Lecturer)
				.ToListAsync();
			return students;
		}

		public async Task<Student?> GetStudentById(int studentId)
		{
			Student? student = await context.Students
				.Where(s => s.StudentId == studentId)
				.Include(s => s.Courses)
				.ThenInclude(c => c.Lecturer)
				.FirstOrDefaultAsync();
			return student;
		}

		public async Task<bool> AddStudent(Student student)
		{
			context.Students.Add(student);
			int saved = await context.SaveChangesAsync();

			return saved > 0;
		}

		public async Task<bool> UpdateStudent(Student student)
		{
			context.Students.Update(student);
			int updated = await context.SaveChangesAsync();

			return updated > 0;
		}

		public async Task<bool> DeleteStudent(Student student)
		{
			context.Students.Remove(student);
			int removed = await context.SaveChangesAsync();

			return removed > 0;
		}
	}
}