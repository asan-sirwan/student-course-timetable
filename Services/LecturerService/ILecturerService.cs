using student_course_timetable.DTOs.LecturerDTOs;

namespace student_course_timetable.Services.LecturerService
{
    public interface ILecturerService
    {
        Task<ServiceResponse<List<LecturerDTO>>> GetLecturers(bool detailed);
		Task<ServiceResponse<LecturerDTO>> GetLecturerById(int id, bool detailed);
		Task<ServiceResponse<LecturerDTO>> AddLecturer(LecturerCreateDTO lecturerCreateDTO);
    }
}