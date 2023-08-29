using student_course_timetable.DTOs.CourseDTOs;
using student_course_timetable.DTOs.LecturerDTOs;
using student_course_timetable.DTOs.StudentDTOs;
using student_course_timetable.Models;
using student_course_timetable.Repositories.CourseRepo;
using student_course_timetable.Repositories.LecturerRepo;

namespace student_course_timetable.Services.CourseService
{
	public class CourseService : ICourseService
	{
		private readonly ICourseRepository courseRepository;
		private readonly ILecturerRepository lecturerRepository;

		public CourseService(ICourseRepository courseRepository, ILecturerRepository lecturerRepository)
		{
			this.courseRepository = courseRepository;
			this.lecturerRepository = lecturerRepository;
		}

		public async Task<ServiceResponse<List<CourseDTO>>> GetCourses(bool detailed)
		{
			List<Course> courses = await courseRepository.GetCourses();

			List<CourseDTO> courseDTOs = courses.Select(course => GetCourseDTO(course, detailed)).ToList();

			return ServiceResponse<List<CourseDTO>>.Success(courseDTOs, 200);
		}

		public async Task<ServiceResponse<CourseDTO>> GetCourseById(int id, bool detailed)
		{
			Course? course = await courseRepository.GetCourseById(id);
			if (course == null)
			{
				return ServiceResponse<CourseDTO>
					.Fail($"Course with id {id} wasn't found.", 404);
			}

			CourseDTO courseDTO = GetCourseDTO(course, detailed);

			return ServiceResponse<CourseDTO>.Success(courseDTO, 200);
		}

		public async Task<ServiceResponse<CourseDTO>> AddCourse(CourseCreateDTO courseCreateDTO)
		{
			Lecturer? courseLecturer = await lecturerRepository.GetLecturerById(courseCreateDTO.LecturerId);
			if (courseLecturer == null)
			{
				return ServiceResponse<CourseDTO>
					.Fail($"Lecturer with id {courseCreateDTO.LecturerId} wasn't found.", 404);
			}

			Course newCourse = new()
			{
				Title = courseCreateDTO.Title,
				Description = courseCreateDTO.Description,
				CourseDateTime = courseCreateDTO.CourseDateTime,
				Lecturer = courseLecturer
			};

			bool saved = await courseRepository.AddCourse(newCourse);
			if (!saved)
			{
				return ServiceResponse<CourseDTO>
		   			.Fail("Failed to add new course.", 500);
			}

			return ServiceResponse<CourseDTO>.Success(GetCourseDTO(newCourse), 201);
		}

		public async Task<ServiceResponse<CourseDTO>> UpdateCourse(CourseUpdateDTO courseUpdateDTO)
		{
			try
			{
				Lecturer? courseLecturer = await lecturerRepository.GetLecturerById(courseUpdateDTO.LecturerId);
				if (courseLecturer == null)
				{
					return ServiceResponse<CourseDTO>
						.Fail($"Lecturer with id {courseUpdateDTO.LecturerId} wasn't found.", 404);
				}

				Course updateCourse = new()
				{
					Id = courseUpdateDTO.Id,
					Title = courseUpdateDTO.Title,
					Description = courseUpdateDTO.Description,
					CourseDateTime = courseUpdateDTO.CourseDateTime,
					Lecturer = courseLecturer
				};

				bool updated = await courseRepository.UpdateCourse(updateCourse);
				if (!updated)
				{
					return ServiceResponse<CourseDTO>
						   .Fail("Failed to update course.", 500);
				}

				return ServiceResponse<CourseDTO>.Success(GetCourseDTO(updateCourse), 200);
			}
			catch (Exception)
			{
				return ServiceResponse<CourseDTO>
					.Fail("Failed to update course.", 304);
			}
		}

		public async Task<ServiceResponse<CourseDTO>> DeleteCourse(int id)
		{
			Course? course = await courseRepository.GetCourseById(id);
			if (course == null)
			{
				return ServiceResponse<CourseDTO>
					.Fail($"Course with id {id} wasn't found.", 404);
			}

			bool removed = await courseRepository.DeleteCourse(course);
			if (!removed)
			{
				return ServiceResponse<CourseDTO>
					.Fail("Failed to remove course.", 500);
			}

			return ServiceResponse<CourseDTO>.Success(GetCourseDTO(course), 200);
		}

		private static CourseDTO GetCourseDTO(Course course, bool detailed = false)
		{
			return new CourseDTO
			{
				Id = course.Id,
				Title = course.Title,
				Description = course.Description,
				CourseDateTime = course.CourseDateTime,
				Lecturer = new LecturerDTO
				{
					Id = course.Lecturer.Id,
					Name = course.Lecturer.Name,
					Email = course.Lecturer.Email,
					BirthDate = course.Lecturer.BirthDate,
					Degree = course.Lecturer.Degree
				},
				Students = detailed ? course.Students.Select(student => new StudentDTO
				{
					Id = student.Id,
					Name = student.Name,
					Email = student.Email,
					BirthDate = student.BirthDate,
					Address = student.Address
				}).ToList() : null
			};
		}
	}
}