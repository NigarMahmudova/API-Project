namespace EducationApp.Service.Dtos.StudentDtos
{
    public class StudentGetDto
    {
        public string FullName { get; set; }
        public decimal Point { get; set; }
        public GroupInStudentGetDto Group { get; set; }
    }

    public class GroupInStudentGetDto
    {
        public int Id { get; set; }
        public string No { get; set; }
    }
}
