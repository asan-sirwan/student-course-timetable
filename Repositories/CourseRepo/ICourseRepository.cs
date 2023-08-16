using student_course_timetable.Models;

namespace student_course_timetable.Repositories.CourseRepo
{
    public interface ICourseRepository
    {
        Task<List<Course>> GetCourses();
		Task<Course?> GetCourseById(int courseId);
		Task<bool> AddCourse(Course course);
    }
}