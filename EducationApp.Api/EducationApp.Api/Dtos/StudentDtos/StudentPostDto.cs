using FluentValidation;

namespace EducationApp.Api.Dtos.StudentDtos
{
    public class StudentPostDto
    {
        public int GroupId { get; set; }
        public string FullName { get; set; }
        public decimal Point { get; set; }
    }

    public class StudentPostDtoValidator : AbstractValidator<StudentPostDto>
    {
        public StudentPostDtoValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().MinimumLength(10).MaximumLength(50);
            RuleFor(x => x.Point).GreaterThanOrEqualTo(0);
        }
    }
}
