namespace EducationApp.UI.ViewModels
{
    public class ErrorVM
    {
        public string Message { get; set; }
        public List<ErrorVMItem> Errors { get; set; }
    }

    public class ErrorVMItem
    {
        public string Key { get; set; }
        public string ErrorMessage { get; set; }
    }
}
