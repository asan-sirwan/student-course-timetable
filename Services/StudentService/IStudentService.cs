using student_course_timetable.DTOs.StudentDTOs;

namespace student_course_timetable.Services.StudentService
{
    public interface IStudentService
    {
        Task<ServiceResponse<List<StudentDTO>>> GetStudents(bool detailed);
		Task<ServiceResponse<StudentDTO>> GetStudentById(int id, bool detailed);
    }
}