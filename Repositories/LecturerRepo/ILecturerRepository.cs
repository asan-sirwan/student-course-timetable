using student_course_timetable.Models;

namespace student_course_timetable.Repositories.LecturerRepo
{
    public interface ILecturerRepository
    {
        Task<List<Lecturer>> GetLecturers();
        Task<Lecturer?> GetLecturerById(int lecturerId);
		Task<bool> AddLecturer(Lecturer lecturer);
		Task<bool> UpdateLecturer(Lecturer lecturer);
		Task<bool> DeleteLecturer(Lecturer lecturer);
    }
}