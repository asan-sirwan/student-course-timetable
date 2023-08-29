using Microsoft.EntityFrameworkCore;
using student_course_timetable.Data;

namespace student_course_timetable;

public class AttachmentRepository : IAttachmentRepository
{
	private readonly DataContext context;

	public AttachmentRepository(DataContext context)
	{
		this.context = context;
	}

	public async Task<bool> AddAttachment(Attachment attachment)
	{
		context.Attachments.Add(attachment);
		int saved = await context.SaveChangesAsync();

		return saved > 0;
	}

	public async Task<bool> DeleteAttachment(Attachment attachment)
	{
		context.Attachments.Remove(attachment);
		int removed = await context.SaveChangesAsync();

		return removed > 0;
	}

	public async Task<Attachment?> GetAttachmentById(int id)
	{
		Attachment? attachment = await context.Attachments
			.Where(a => a.Id == id)
			.Include(a => a.Course)
			.FirstOrDefaultAsync();
		return attachment;
	}

	public async Task<List<Attachment>> GetAttachmentsByCourse(int courseId)
	{
		List<Attachment> attachments = await context.Attachments
			.Where(a => a.Course.Id == courseId)
			.Include(a => a.Course)
			.ToListAsync();
		return attachments;
	}

	public async Task<bool> UpdateAttachment(Attachment attachment)
	{
		var trackedAttachment = await context.Attachments.FindAsync(attachment.Id);
		if (trackedAttachment == null)
		{
			return false;
		}

		trackedAttachment.Title = attachment.Title;
		trackedAttachment.File = attachment.File;
		trackedAttachment.Course = attachment.Course;

		int updated = await context.SaveChangesAsync();

		return updated > 0;
	}
}
