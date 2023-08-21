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
				.Where(l => l.Id == lecturerId)
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
			var trackedLecturer = await context.Lecturers.FindAsync(lecturer.Id);
			if (trackedLecturer == null)
			{
				return false;
			}

			trackedLecturer.Name = lecturer.Name;
			trackedLecturer.Email = lecturer.Email;
			trackedLecturer.Password = lecturer.Password;
			trackedLecturer.BirthDate = lecturer.BirthDate; 
			trackedLecturer.Degree = lecturer.Degree; 
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