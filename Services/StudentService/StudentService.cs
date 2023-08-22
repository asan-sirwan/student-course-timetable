using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
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


		public async Task<ServiceResponse<List<StudentDTO>>> GetStudentsWithCourses(bool detailed)
		{
			List<Student> students = await studentRepository.GetStudentsWithCourses();

			List<StudentDTO> studentDTOs = students.Select(student => GetStudentDTO(student, detailed)).ToList();

			return ServiceResponse<List<StudentDTO>>.Success(studentDTOs, 200);
		}

		public async Task<ServiceResponse<StudentDTO>> StudentLogin(StudentLoginDTO studentLoginDTO)
		{
			try
			{
				string email = studentLoginDTO.Email;
				string password = studentLoginDTO.Password;
				
				Student? student = await studentRepository.StudentLogin(email, password);
				if (student == null)
				{
					return ServiceResponse<StudentDTO>
						.Fail("Email or Password is incorrect.", 400);
				}

				return ServiceResponse<StudentDTO>
					.Success(GetStudentDTO(student, detailed: true), 200);
			}
			catch (Exception)
			{
				return ServiceResponse<StudentDTO>
					.Fail("Something went wrong.", 500);
			}
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

		public async Task<ServiceResponse<StudentDTO>> UpdateStudent(StudentUpdateDTO studentUpdateDTO)
		{
			try
			{
				Student updateStudent = new()
				{
					Id = studentUpdateDTO.Id,
					Name = studentUpdateDTO.Name,
					Email = studentUpdateDTO.Email,
					Password = studentUpdateDTO.Password,
					BirthDate = studentUpdateDTO.BirthDate,
					Address = studentUpdateDTO.Address
				};

				bool updated = await studentRepository.UpdateStudent(updateStudent);
				if (!updated)
				{
					return ServiceResponse<StudentDTO>
						   .Fail("Failed to update student.", 500);
				}

				return ServiceResponse<StudentDTO>.Success(GetStudentDTO(updateStudent), 200);
			}
			catch (Exception)
			{
				return ServiceResponse<StudentDTO>
					.Fail("Failed to update student.", 304);
			}
		}

		public async Task<ServiceResponse<StudentDTO>> DeleteStudent(int id)
		{
			Student? student = await studentRepository.GetStudentById(id);
			if (student == null)
			{
				return ServiceResponse<StudentDTO>
					.Fail($"Student with id {id} wasn't found.", 404);
			}

			bool removed = await studentRepository.DeleteStudent(student);
			if (!removed)
			{
				return ServiceResponse<StudentDTO>
					.Fail("Failed to remove student.", 500);
			}

			return ServiceResponse<StudentDTO>.Success(GetStudentDTO(student), 200);
		}

		private static StudentDTO GetStudentDTO(Student student, bool detailed = false)
		{
			return new StudentDTO
			{
				Id = student.Id,
				Name = student.Name,
				Email = student.Email,
				BirthDate = student.BirthDate,
				Address = student.Address,
				Courses = detailed ? student.Courses.Select(course => new CourseDTO
				{
					Id = course.Id,
					Title = course.Title,
					Description = course.Description,
					CourseDateTime = course.CourseDateTime,
					Lecturer = new()
					{
						Id = course.Lecturer.Id,
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