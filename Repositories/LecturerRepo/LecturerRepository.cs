using Microsoft.EntityFrameworkCore;
using student_course_timetable.Data;
using student_course_timetable.Models;

namespace student_course_timetable.Repositories.LecturerRepo
{
	public class LecturerRepository : ILecturerRepository
	{
		private readonly DataContext context;

		public LecturerRepository(DataContext context)
		{
			this.context = context;
		}

		public async Task<List<Lecturer>> GetLecturers()
		{
			List<Lecturer> lecturers = await context.Lecturers
			.Include(l => l.Courses)
			.ToListAsync();
			return lecturers;
		}
	}
}