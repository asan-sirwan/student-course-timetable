using student_course_timetable.Models;

namespace student_course_timetable.Services.AssignService
{
    public interface IAssignService
    {
        Task<ServiceResponse<bool>> AssignStudentToCourse(StudentCourse studentCourse);
		Task<ServiceResponse<bool>> DeleteAssignment(int studentId, int courseId);
    }
}