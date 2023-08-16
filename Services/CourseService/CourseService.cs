using student_course_timetable.DTOs.CourseDTOs;
using student_course_timetable.DTOs.LecturerDTOs;
using student_course_timetable.DTOs.StudentDTOs;
using student_course_timetable.Models;
using student_course_timetable.Repositories.CourseRepo;

namespace student_course_timetable.Services.CourseService
{
	public class CourseService : ICourseService
	{
		private readonly ICourseRepository courseRepository;

		public CourseService(ICourseRepository courseRepository)
		{
			this.courseRepository = courseRepository;
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

		private static CourseDTO GetCourseDTO(Course course, bool detailed)
		{
			return new CourseDTO
			{
				CourseId = course.CourseId,
				Title = course.Title,
				Description = course.Description,
				CourseDateTime = course.CourseDateTime,
				Lecturer = new LecturerDTO
				{
					LecturerId = course.Lecturer.LecturerId,
					Name = course.Lecturer.Name,
					Email = course.Lecturer.Email,
					BirthDate = course.Lecturer.BirthDate,
					Degree = course.Lecturer.Degree
				},
				Students = detailed ? course.Students.Select(student => new StudentDTO
				{
					StudentId = student.StudentId,
					Name = student.Name,
					Email = student.Email,
					BirthDate = student.BirthDate,
					Address = student.Address
				}).ToList() : null
			};
		}
	}
}