using System.ComponentModel.DataAnnotations;

namespace EducationApp.UI.ViewModels
{
    public class GroupEditVM
    {
        [Required]
        [MaxLength(5)]
        [MinLength(4)]
        public string No { get; set; }
    }
}
