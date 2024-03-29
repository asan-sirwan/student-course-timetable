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

		public async Task<List<Student>> GetStudentsWithCourses()
		{
			List<Student> students = await context.Students
				.Include(s => s.Courses)
				.ThenInclude(c => c.Lecturer)
				.Where(s => s.Courses.Any())
				.ToListAsync();
			return students;
		}

		public async Task<Student?> StudentLogin(string email, string password)
		{
			Student? student = await context.Students
				.Where(s => s.Email == email && s.Password == password)
				.Include(s => s.Courses)
				.ThenInclude(c => c.Lecturer)
				.FirstOrDefaultAsync();
			return student;
		}

		public async Task<Student?> GetStudentById(int studentId)
		{
			Student? student = await context.Students
				.Where(s => s.Id == studentId)
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
			var trackedStudent = await context.Students.FindAsync(student.Id);
			if (trackedStudent == null)
			{
				return false;
			}

			trackedStudent.Name = student.Name;
			trackedStudent.Email = student.Email;
			trackedStudent.Password = student.Password;
			trackedStudent.BirthDate = student.BirthDate;
			trackedStudent.Address = student.Address;
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