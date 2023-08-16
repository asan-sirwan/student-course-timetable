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

		public async Task<ServiceResponse<LecturerDTO>> AddLecturer(LecturerCreateDTO lecturerCreateDTO)
		{
			Lecturer newLecturer = new()
			{
				Name = lecturerCreateDTO.Name,
				Email = lecturerCreateDTO.Email,
				Password = lecturerCreateDTO.Password,
				BirthDate = lecturerCreateDTO.BirthDate,
				Degree = lecturerCreateDTO.Degree,
			};

			bool saved = await lecturerRepository.AddLecturer(newLecturer);
			if (!saved)
			{
				return ServiceResponse<LecturerDTO>
		   			.Fail("Failed to add new lecturer.", 500);
			}

			return ServiceResponse<LecturerDTO>.Success(GetLecturerDTO(newLecturer), 201);
		}

		private static LecturerDTO GetLecturerDTO(Lecturer lecturer, bool detailed = false)
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