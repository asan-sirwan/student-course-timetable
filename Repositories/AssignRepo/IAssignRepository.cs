namespace student_course_timetable.Repositories.AssignRepo
{
    public interface IAssignRepository
    {
        Task<bool> AssignStudentToCourse(int studentId, int courseId);
		Task<bool> DeleteAssignment(int studentId, int courseId);
    }
}