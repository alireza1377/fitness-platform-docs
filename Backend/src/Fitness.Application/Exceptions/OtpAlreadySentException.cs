namespace Fitness.Application.Exceptions;

public sealed class OtpAlreadySentException : AppException
{
    public OtpAlreadySentException()
        : base(
            "کد تأیید قبلاً ارسال شده است.",
            "OTP_ALREADY_SENT")
    {
    }
}