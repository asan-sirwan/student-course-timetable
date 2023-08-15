using student_course_timetable.DTOs.CourseDTOs;
using student_course_timetable.DTOs.LecturerDTOs;
using student_course_timetable.Models;
using student_course_timetable.Repositories.LecturerRepo;

namespace student_course_timetable.Services.LecturerService
{
	public class LecturerService : ILecturerService
	{
		private readonly ILecturerRepository lecturerRepository;

		public LecturerService(ILecturerRepository lecturerRepository)
		{
			this.lecturerRepository = lecturerRepository;
		}

		public async Task<List<LecturerDTO>> GetLecturers(bool detailed)
		{
			List<Lecturer> lecturers = await lecturerRepository.GetLecturers();

			List<LecturerDTO> lecturerDTOs = lecturers.Select(lecturer => new LecturerDTO
			{
				LecturerId = lecturer.LecturerId,
				Name = lecturer.Name,
				Email = lecturer.Email,
				BirthDate = lecturer.BirthDate,
				Degree = lecturer.Degree,
				Courses = detailed ? lecturer.Courses.Select(course => new CourseDTO
				{
					CourseId = course.CourseId,
					Title = course.Title,
					Description = course.Description,
					CourseDateTime = course.CourseDateTime
				}).ToList() : null
			}).ToList();

			return lecturerDTOs;
		}
	}
}