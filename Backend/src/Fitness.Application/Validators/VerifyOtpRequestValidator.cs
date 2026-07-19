using Fitness.Application.DTOs.Auth;
using FluentValidation;

namespace Fitness.Application.Validators;

public sealed class VerifyOtpRequestValidator
    : AbstractValidator<VerifyOtpRequest>
{
    public VerifyOtpRequestValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage("شماره موبایل الزامی است.")

            .Length(11)
            .WithMessage("شماره موبایل باید 11 رقم باشد.")

            .Matches("^09\\d{9}$")
            .WithMessage("فرمت شماره موبایل معتبر نیست.");

        RuleFor(x => x.Code)
            .NotEmpty()
            .WithMessage("کد تأیید الزامی است.")

            .Length(6)
            .WithMessage("کد تأیید باید 6 رقم باشد.")

            .Matches("^\\d{6}$")
            .WithMessage("کد تأیید فقط باید شامل عدد باشد.");
    }
}