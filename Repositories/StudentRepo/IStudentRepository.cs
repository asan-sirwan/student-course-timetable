using student_course_timetable.Models;

namespace student_course_timetable.Repositories.StudentRepo
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetStudents();
		Task<Student?> GetStudentById(int studentId);
		Task<bool> AddStudent(Student student);
    }
}