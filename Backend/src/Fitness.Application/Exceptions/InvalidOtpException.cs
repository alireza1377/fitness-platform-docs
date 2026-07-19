namespace Fitness.Application.Exceptions;

public sealed class InvalidOtpException : AppException
{
    public InvalidOtpException()
        : base(
            "کد تأیید صحیح نیست.",
            "INVALID_OTP")
    {
    }
}