using student_course_timetable.DTOs.CourseDTOs;
using student_course_timetable.Models;
using student_course_timetable.Repositories.CourseRepo;
using student_course_timetable.Services;

namespace student_course_timetable;

public class AttachmentService : IAttachmentService
{
	private readonly IAttachmentRepository attachmentRepository;
	private readonly ICourseRepository courseRepository;

	public AttachmentService(IAttachmentRepository attachmentRepository, ICourseRepository courseRepository)
	{
		this.attachmentRepository = attachmentRepository;
		this.courseRepository = courseRepository;
	}

	public async Task<ServiceResponse<AttachmentDTO>> AddAttachment(AttachmentCreateDTO attachmentCreateDTO)
	{
		Course? attachmentCourse = await courseRepository.GetCourseById(attachmentCreateDTO.CourseId);
		if (attachmentCourse == null)
		{
			return ServiceResponse<AttachmentDTO>
				.Fail($"Course with id {attachmentCreateDTO.CourseId} wasn't found.", 404);
		}

		Attachment newAttachment = new()
		{
			Title = attachmentCreateDTO.Title,
			PostDate = attachmentCreateDTO.PostDate,
			File = attachmentCreateDTO.File,
			Course = attachmentCourse
		};

		bool saved = await attachmentRepository.AddAttachment(newAttachment);
		if (!saved)
		{
			return ServiceResponse<AttachmentDTO>
				   .Fail("Failed to add new attachment.", 500);
		}

		return ServiceResponse<AttachmentDTO>.Success(GetAttachmentDTO(newAttachment), 201);
	}

	public async Task<ServiceResponse<AttachmentDTO>> DeleteAttachment(int id)
	{
		Attachment? attachment = await attachmentRepository.GetAttachmentById(id);
		if (attachment == null)
		{
			return ServiceResponse<AttachmentDTO>
				.Fail($"Attachment with id {id} wasn't found.", 404);
		}

		bool removed = await attachmentRepository.DeleteAttachment(attachment);
		if (!removed)
		{
			return ServiceResponse<AttachmentDTO>
				.Fail("Failed to remove attachment.", 500);
		}

		return ServiceResponse<AttachmentDTO>.Success(GetAttachmentDTO(attachment), 200);
	}

	public async Task<ServiceResponse<AttachmentDTO>> GetAttachmentById(int id)
	{
		Attachment? attachment = await attachmentRepository.GetAttachmentById(id);
		if (attachment == null)
		{
			return ServiceResponse<AttachmentDTO>
				.Fail($"Attachment with id {id} wasn't found.", 404);
		}

		AttachmentDTO attachmentDTO = GetAttachmentDTO(attachment);

		return ServiceResponse<AttachmentDTO>.Success(attachmentDTO, 200);
	}

	public async Task<ServiceResponse<List<AttachmentDTO>>> GetAttachmentsByCourse(int courseId)
	{
		List<Attachment> attachments = await attachmentRepository.GetAttachmentsByCourse(courseId);

		List<AttachmentDTO> attachmentDTOs = attachments.Select(GetAttachmentDTO).ToList();

		return ServiceResponse<List<AttachmentDTO>>.Success(attachmentDTOs, 200);
	}

	public async Task<ServiceResponse<AttachmentDTO>> UpdateAttachment(AttachmentUpdateDTO attachmentUpdateDTO)
	{
		try
		{
			Course? attachmentCourse = await courseRepository.GetCourseById(attachmentUpdateDTO.CourseId);
			if (attachmentCourse == null)
			{
				return ServiceResponse<AttachmentDTO>
					.Fail($"Course with id {attachmentUpdateDTO.CourseId} wasn't found.", 404);
			}

			Attachment updateAttachment = new()
			{
				Id = attachmentUpdateDTO.Id,
				Title = attachmentUpdateDTO.Title,
				PostDate = attachmentUpdateDTO.PostDate,
				File = attachmentUpdateDTO.File,
				Course = attachmentCourse
			};

			bool updated = await attachmentRepository.UpdateAttachment(updateAttachment);
			if (!updated)
			{
				return ServiceResponse<AttachmentDTO>
					   .Fail("Failed to update attachment.", 500);
			}

			return ServiceResponse<AttachmentDTO>.Success(GetAttachmentDTO(updateAttachment), 200);
		}
		catch (Exception)
		{
			return ServiceResponse<AttachmentDTO>
				.Fail("Failed to update attachment.", 304);
		}
	}

	private static AttachmentDTO GetAttachmentDTO(Attachment attachment)
	{
		return new AttachmentDTO
		{
			Id = attachment.Id,
			Title = attachment.Title,
			PostDate = attachment.PostDate,
			File = attachment.File,
			Course = new CourseDTO
			{
				Id = attachment.Course.Id,
				Title = attachment.Course.Title,
				Description = attachment.Course.Description,
				CourseDateTime = attachment.Course.CourseDateTime,
			}
		};
	}
}
