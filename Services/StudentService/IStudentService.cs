using student_course_timetable.DTOs.StudentDTOs;

namespace student_course_timetable.Services.StudentService
{
    public interface IStudentService
    {
        Task<ServiceResponse<List<StudentDTO>>> GetStudents(bool detailed);
		Task<ServiceResponse<List<StudentDTO>>> GetStudentsWithCourses(bool detailed);
		Task<ServiceResponse<StudentDTO>> StudentLogin(StudentLoginDTO studentLoginDTO);
		Task<ServiceResponse<StudentDTO>> GetStudentById(int id, bool detailed);
		Task<ServiceResponse<StudentDTO>> AddStudent(StudentCreateDTO studentCreateDTO);
		Task<ServiceResponse<StudentDTO>> UpdateStudent(StudentUpdateDTO studentUpdateDTO);
		Task<ServiceResponse<StudentDTO>> DeleteStudent(int id);
    }
}