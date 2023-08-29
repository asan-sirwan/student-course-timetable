using student_course_timetable.Services;

namespace student_course_timetable;

public interface IAttachmentService
{
	Task<ServiceResponse<List<AttachmentDTO>>> GetAttachmentsByCourse(int courseId);
	Task<ServiceResponse<AttachmentDTO>> GetAttachmentById(int id);
	Task<ServiceResponse<AttachmentDTO>> AddAttachment(AttachmentCreateDTO attachmentCreateDTO);
	Task<ServiceResponse<AttachmentDTO>> UpdateAttachment(AttachmentUpdateDTO attachmentUpdateDTO);
	Task<ServiceResponse<AttachmentDTO>> DeleteAttachment(int id);
}
