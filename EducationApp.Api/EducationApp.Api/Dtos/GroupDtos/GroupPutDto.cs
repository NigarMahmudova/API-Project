using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace EducationApp.Api.Dtos.GroupDtos
{
    public class GroupPutDto
    {
        public string No { get; set; }
    }

    public class GroupPutDtoValidator : AbstractValidator<GroupPutDto>
    {
        public GroupPutDtoValidator()
        {
            RuleFor(x=>x.No).NotEmpty().MinimumLength(4).MaximumLength(5);
        }
    }
}
