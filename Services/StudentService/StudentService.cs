using student_course_timetable.DTOs.CourseDTOs;
using student_course_timetable.DTOs.StudentDTOs;
using student_course_timetable.Models;
using student_course_timetable.Repositories.StudentRepo;

namespace student_course_timetable.Services.StudentService
{
	public class StudentService : IStudentService
	{
		private readonly IStudentRepository studentRepository;

		public StudentService(IStudentRepository studentRepository)
		{
			this.studentRepository = studentRepository;
		}

		public async Task<ServiceResponse<List<StudentDTO>>> GetStudents(bool detailed)
		{
			List<Student> students = await studentRepository.GetStudents();

			List<StudentDTO> studentDTOs = students.Select(student => GetStudentDTO(student, detailed)).ToList();

			return ServiceResponse<List<StudentDTO>>.Success(studentDTOs, 200);
		}

		public async Task<ServiceResponse<StudentDTO>> GetStudentById(int id, bool detailed)
		{
			Student? student = await studentRepository.GetStudentById(id);
			if (student == null)
			{
				return ServiceResponse<StudentDTO>
					.Fail($"Student with id {id} wasn't found.", 404);
			}

			StudentDTO studentDTO = GetStudentDTO(student, detailed);

			return ServiceResponse<StudentDTO>.Success(studentDTO, 200);
		}

		public async Task<ServiceResponse<StudentDTO>> AddStudent(StudentCreateDTO studentCreateDTO)
		{
			Student newStudent = new()
			{
				Name = studentCreateDTO.Name,
				Email = studentCreateDTO.Email,
				Password = studentCreateDTO.Password,
				BirthDate = studentCreateDTO.BirthDate,
				Address = studentCreateDTO.Address
			};

			bool saved = await studentRepository.AddStudent(newStudent);
			if (!saved)
			{
				return ServiceResponse<StudentDTO>
		   			.Fail("Failed to add new student.", 500);
			}

			return ServiceResponse<StudentDTO>.Success(GetStudentDTO(newStudent), 200);
		}

		private static StudentDTO GetStudentDTO(Student student, bool detailed = false)
		{
			return new StudentDTO
			{
				StudentId = student.StudentId,
				Name = student.Name,
				Email = student.Email,
				BirthDate = student.BirthDate,
				Address = student.Address,
				Courses = detailed ? student.Courses.Select(course => new CourseDTO
				{
					CourseId = course.CourseId,
					Title = course.Title,
					Description = course.Description,
					CourseDateTime = course.CourseDateTime,
					Lecturer = new()
					{
						LecturerId = course.Lecturer.LecturerId,
						Name = course.Lecturer.Name,
						Email = course.Lecturer.Email,
						BirthDate = course.Lecturer.BirthDate,
						Degree = course.Lecturer.Degree,
					}
				}).ToList() : null
			};
		}
	}
}