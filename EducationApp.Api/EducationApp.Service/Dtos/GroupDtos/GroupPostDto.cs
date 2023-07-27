using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace EducationApp.Service.Dtos.GroupDtos
{
    public class GroupPostDto
    {
        public string No { get; set; }
    }

    public class GroupPostDtoValidator : AbstractValidator<GroupPostDto>
    {
        public GroupPostDtoValidator()
        {
            RuleFor(x => x.No).NotEmpty().MinimumLength(4).MaximumLength(5);
        }
    }
}
