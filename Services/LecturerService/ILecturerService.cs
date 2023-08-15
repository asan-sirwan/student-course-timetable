using student_course_timetable.DTOs.LecturerDTOs;

namespace student_course_timetable.Services.LecturerService
{
    public interface ILecturerService
    {
        Task<List<LecturerDTO>> GetLecturers(bool detailed);
    }
}