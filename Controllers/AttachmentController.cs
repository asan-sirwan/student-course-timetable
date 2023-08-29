using Microsoft.AspNetCore.Mvc;
using student_course_timetable.Services;

namespace student_course_timetable;

[ApiController]
[Route("/attachments")]
public class AttachmentController : ControllerBase
{
	private readonly IAttachmentService attachmentService;

	public AttachmentController(IAttachmentService attachmentService)
	{
		this.attachmentService = attachmentService;
	}

	[HttpGet("course/{id}")]
	public async Task<ActionResult<ServiceResponse<List<AttachmentDTO>>>> GetAttachmentsByCourse([FromQuery] int id)
	{
		ServiceResponse<List<AttachmentDTO>> attachments = await attachmentService.GetAttachmentsByCourse(id);
		if (!attachments.IsSuccess)
		{ return StatusCode(attachments.StatusCode, attachments); }

		return Ok(attachments.Data);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<ServiceResponse<AttachmentDTO>>> GetAttachmentById([FromQuery] int id)
	{
		ServiceResponse<AttachmentDTO> attachments = await attachmentService.GetAttachmentById(id);
		if (!attachments.IsSuccess)
		{ return StatusCode(attachments.StatusCode, attachments); }

		return Ok(attachments.Data);
	}

	[HttpPost]
	public async Task<ActionResult<ServiceResponse<AttachmentDTO>>> AddAttachment([FromForm] AttachmentCreateDTO newAttachment)
	{
		try
		{
			if (!ModelState.IsValid)
			{
				BadRequest(ServiceResponse<AttachmentDTO>.Fail("Bad input", 400));
			}

			ServiceResponse<AttachmentDTO> attachment = await attachmentService.AddAttachment(newAttachment);
			if (!attachment.IsSuccess)
			{ return StatusCode(attachment.StatusCode, attachment); }

			return CreatedAtAction(nameof(GetAttachmentById), new { id = attachment.Data!.Id }, attachment.Data);
		}
		catch (Exception)
		{
			var error = ServiceResponse<AttachmentDTO>.Fail("Something went wrong :(", 500);
			return StatusCode(error.StatusCode, error);
		}
	}

	[HttpPut]
	public async Task<ActionResult<ServiceResponse<AttachmentDTO>>> UpdateAttachment([FromBody] AttachmentUpdateDTO updateAttachment)
	{
		try
		{
			if (!ModelState.IsValid)
			{
				BadRequest(ServiceResponse<AttachmentDTO>.Fail("Bad input", 400));
			}

			ServiceResponse<AttachmentDTO> attachment = await attachmentService.UpdateAttachment(updateAttachment);
			if (!attachment.IsSuccess)
			{ return StatusCode(attachment.StatusCode); }

			return Ok(attachment.Data);
		}
		catch (Exception)
		{
			var error = ServiceResponse<AttachmentDTO>.Fail("Something went wrong :(", 500);
			return StatusCode(error.StatusCode, error);
		}
	}

	[HttpDelete("{id}")]
	public async Task<ActionResult<ServiceResponse<AttachmentDTO>>> DeleteAttachment(int id)
	{
		try
		{
			if (!ModelState.IsValid)
			{
				BadRequest(ServiceResponse<AttachmentDTO>.Fail("Bad input", 400));
			}

			ServiceResponse<AttachmentDTO> attachment = await attachmentService.DeleteAttachment(id);
			if (!attachment.IsSuccess)
			{ return StatusCode(attachment.StatusCode, attachment); }

			return Ok(attachment.Data);
		}
		catch (Exception)
		{
			var error = ServiceResponse<AttachmentDTO>.Fail("Something went wrong :(", 500);
			return StatusCode(error.StatusCode, error);
		}
	}
}
