namespace Fitness.Application.Exceptions;

public sealed class TooManyOtpAttemptsException : AppException
{
    public TooManyOtpAttemptsException()
        : base(
            "تعداد تلاش‌های وارد کردن کد بیش از حد مجاز است.",
            "OTP_MAX_ATTEMPTS")
    {
    }
}