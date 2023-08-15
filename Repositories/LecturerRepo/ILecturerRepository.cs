using student_course_timetable.Models;

namespace student_course_timetable.Repositories.LecturerRepo
{
    public interface ILecturerRepository
    {
        Task<List<Lecturer>> GetLecturers();
    }
}