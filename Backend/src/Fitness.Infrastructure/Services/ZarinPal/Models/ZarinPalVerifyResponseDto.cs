namespace Fitness.Infrastructure.Services.ZarinPal.Models;

public class ZarinPalVerifyResponseDto
{
    public DataDto? Data { get; set; }

    public ErrorDto? Errors { get; set; }

    public class DataDto
    {
        public int Code { get; set; }

        public long RefId { get; set; }

        public decimal Fee { get; set; }

        public string FeeType { get; set; } = string.Empty;
    }

    public class ErrorDto
    {
        public int Code { get; set; }

        public string Message { get; set; } = string.Empty;
    }
}