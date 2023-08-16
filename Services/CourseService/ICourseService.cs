using student_course_timetable.DTOs.CourseDTOs;

namespace student_course_timetable.Services.CourseService
{
    public interface ICourseService
    {
        Task<ServiceResponse<List<CourseDTO>>> GetCourses(bool detailed);
		Task<ServiceResponse<CourseDTO>> GetCourseById(int id, bool detailed);
    }
}