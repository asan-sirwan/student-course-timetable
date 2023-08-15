using Microsoft.EntityFrameworkCore;

namespace student_course_timetable.Models
{
	[PrimaryKey(nameof(StudentId), nameof(CourseId))]
    public class StudentCourse
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
    }
}