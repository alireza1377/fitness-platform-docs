namespace Fitness.Application.Exceptions;

public sealed class OtpExpiredException : AppException
{
    public OtpExpiredException()
        : base(
            "کد تأیید منقضی شده است.",
            "OTP_EXPIRED")
    {
    }
}