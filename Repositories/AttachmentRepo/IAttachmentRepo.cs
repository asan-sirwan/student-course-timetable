namespace student_course_timetable;

public interface IAttachmentRepository
{
	Task<List<Attachment>> GetAttachmentsByCourse(int courseId);
	Task<Attachment?> GetAttachmentById(int id);
	Task<bool> AddAttachment(Attachment attachment);
	Task<bool> UpdateAttachment(Attachment attachment);
	Task<bool> DeleteAttachment(Attachment attachment);
}
