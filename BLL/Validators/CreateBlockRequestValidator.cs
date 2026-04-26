using Domain.Models;
using FluentValidation;

namespace BLL.Validators
{
    public class CreateBlockRequestValidator : AbstractValidator<CreateBlockRequest>
    {
        public CreateBlockRequestValidator()
        {
            RuleFor(x => x.DataText)
                .NotEmpty().WithMessage("Текст документа не може бути порожнім.")
                .MinimumLength(5).WithMessage("Текст має містити мінімум 5 символів.")
                .MaximumLength(1000).WithMessage("Текст занадто довгий.");
        }
    }
}