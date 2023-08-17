using student_course_timetable.Models;
using student_course_timetable.Repositories.AssignRepo;

namespace student_course_timetable.Services.AssignService
{
	public class AssignService : IAssignService
	{
		private readonly IAssignRepository assignRepository;

		public AssignService(IAssignRepository assignRepository)
		{
			this.assignRepository = assignRepository;
		}

		public async Task<ServiceResponse<bool>> AssignStudentToCourse(StudentCourse studentCourse)
		{
			int studentId = studentCourse.StudentId;
			int courseId = studentCourse.CourseId;

			bool saved = await assignRepository.AssignStudentToCourse(studentId, courseId);
			if (!saved)
			{
				return ServiceResponse<bool>
					.Fail("Counldn't assign course to student, check the IDs.", 500);
			}

			return ServiceResponse<bool>.Success(saved, 201);
		}

		public async Task<ServiceResponse<bool>> DeleteAssignment(int studentId, int courseId)
		{
			bool removed = await assignRepository.DeleteAssignment(studentId, courseId);
			if (!removed)
			{
				return ServiceResponse<bool>
					.Fail("Counldn't remove the assignment, check the IDs.", 500);
			}

			return ServiceResponse<bool>.Success(removed, 201);
		}
	}
}