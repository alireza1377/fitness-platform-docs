namespace Fitness.Infrastructure.Services.ZarinPal.Models;

public class ZarinPalRequestResponseDto
{
    public DataDto? Data { get; set; }

    public ErrorDto? Errors { get; set; }

    public class DataDto
    {
        public int Code { get; set; }

        public string Authority { get; set; } = string.Empty;

        public string FeeType { get; set; } = string.Empty;

        public decimal Fee { get; set; }
    }

    public class ErrorDto
    {
        public int Code { get; set; }

        public string Message { get; set; } = string.Empty;
    }
}