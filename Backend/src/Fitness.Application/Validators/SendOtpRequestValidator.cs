using Fitness.Application.DTOs.Auth;
using FluentValidation;

namespace Fitness.Application.Validators;

public sealed class SendOtpRequestValidator
    : AbstractValidator<SendOtpRequest>
{
    public SendOtpRequestValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage("شماره موبایل الزامی است.")

            .Length(11)
            .WithMessage("شماره موبایل باید 11 رقم باشد.")

            .Matches("^09\\d{9}$")
            .WithMessage("فرمت شماره موبایل معتبر نیست.");
    }
}