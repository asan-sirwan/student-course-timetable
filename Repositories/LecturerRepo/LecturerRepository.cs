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

		public async Task<Lecturer?> GetLecturerById(int lecturerId)
		{
			Lecturer? lecturer = await context.Lecturers
				.Where(l => l.LecturerId == lecturerId)
				.Include(l => l.Courses)
				.FirstOrDefaultAsync();
			return lecturer;
		}

		public async Task<bool> AddLecturer(Lecturer lecturer)
		{
			context.Lecturers.Add(lecturer);
			int saved = await context.SaveChangesAsync();

			return saved > 0;
		}

		public async Task<bool> UpdateLecturer(Lecturer lecturer)
		{
			context.Lecturers.Update(lecturer);
			int updated = await context.SaveChangesAsync();

			return updated > 0;
		}

		public async Task<bool> DeleteLecturer(Lecturer lecturer)
		{
			context.Lecturers.Remove(lecturer);
			int removed = await context.SaveChangesAsync();

			return removed > 0;
		}
	}
}