namespace UniversityManagementSystem.Models
{
    public class CourseAssignment
    {
        public int instructorID { get; set; }
        public int CourseID { get; set; }
        public Instructor Instructor { get; set; }
        public Course Course { get; set; }
    }
}
