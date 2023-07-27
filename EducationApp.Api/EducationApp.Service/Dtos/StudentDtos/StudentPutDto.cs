using FluentValidation;

namespace EducationApp.Service.Dtos.StudentDtos
{
    public class StudentPutDto
    {
        public int GroupId { get; set; }
        public string FullName { get; set; }
        public decimal Point { get; set; }
    }

    public class StudentPutDtoValidator : AbstractValidator<StudentPutDto>
    {
        public StudentPutDtoValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().MinimumLength(10).MaximumLength(50);
            RuleFor(x => x.Point).GreaterThanOrEqualTo(0);
        }
    }
}
