using student_course_timetable.Models;

namespace student_course_timetable.Repositories.StudentRepo
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetStudents();
        Task<List<Student>> GetStudentsWithCourses();
		Task<Student?> GetStudentById(int studentId);
		Task<bool> AddStudent(Student student);
		Task<bool> UpdateStudent(Student student);
		Task<bool> DeleteStudent(Student student);
    }
}