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

		public async Task<ServiceResponse<List<LecturerDTO>>> GetLecturers(bool detailed)
		{
			List<Lecturer> lecturers = await lecturerRepository.GetLecturers();

			List<LecturerDTO> lecturerDTOs = lecturers.Select(lecturer => GetLecturerDTO(lecturer, detailed)).ToList();

			return ServiceResponse<List<LecturerDTO>>.Success(lecturerDTOs, 200);
		}

		public async Task<ServiceResponse<LecturerDTO>> GetLecturerById(int id, bool detailed)
		{
			Lecturer? lecturer = await lecturerRepository.GetLecturerById(id);
			if (lecturer == null)
			{
				return ServiceResponse<LecturerDTO>
					.Fail($"Lecturer with id {id} wasn't found.", 404);
			}

			LecturerDTO lecturerDTO = GetLecturerDTO(lecturer, detailed);

			return ServiceResponse<LecturerDTO>.Success(lecturerDTO, 200);
		}

		private static LecturerDTO GetLecturerDTO(Lecturer lecturer, bool detailed)
		{
			return new LecturerDTO
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
			};
		}
	}
}