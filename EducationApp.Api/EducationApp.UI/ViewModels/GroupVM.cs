namespace EducationApp.UI.ViewModels
{
    public class GroupVM
    {
        public List<GroupVMItem> Groups { get; set; }

        public class GroupVMItem
        {
            public int Id { get; set; }
            public string No { get; set; }
        }
    }
}
