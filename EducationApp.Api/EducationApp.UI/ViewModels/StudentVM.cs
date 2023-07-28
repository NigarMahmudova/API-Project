namespace EducationApp.UI.ViewModels
{
    public class StudentVM
    {
        public List<StudentVMItem> Students { get; set; }

        public class StudentVMItem
        {
            public int Id { get; set; }
            public string FullName { get; set; }
            public decimal Point { get; set; }
            public string GroupNo { get; set; }
        }
    }
}
