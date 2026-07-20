using Fitness.Application.DTOs.Profile;
using FluentValidation;

namespace Fitness.Application.Validators.Profile;

public class UpdateProfileRequestValidator
    : AbstractValidator<UpdateProfileRequest>
{
    public UpdateProfileRequestValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("نام الزامی است.")
            .MaximumLength(50)
            .WithMessage("نام نمی‌تواند بیشتر از 50 کاراکتر باشد.");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("نام خانوادگی الزامی است.")
            .MaximumLength(50)
            .WithMessage("نام خانوادگی نمی‌تواند بیشتر از 50 کاراکتر باشد.");

        RuleFor(x => x.Email)
            .EmailAddress()
            .When(x => !string.IsNullOrWhiteSpace(x.Email))
            .WithMessage("ایمیل معتبر نیست.");

        RuleFor(x => x.AvatarUrl)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.AvatarUrl))
            .WithMessage("آدرس تصویر بیش از حد طولانی است.");

        RuleFor(x => x.Height)
            .InclusiveBetween(50, 300)
            .When(x => x.Height.HasValue)
            .WithMessage("قد باید بین 50 تا 300 سانتی‌متر باشد.");

        RuleFor(x => x.Weight)
            .InclusiveBetween(20, 500)
            .When(x => x.Weight.HasValue)
            .WithMessage("وزن باید بین 20 تا 500 کیلوگرم باشد.");

        RuleFor(x => x.BirthDate)
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
            .When(x => x.BirthDate.HasValue)
            .WithMessage("تاریخ تولد معتبر نیست.");
    }
}