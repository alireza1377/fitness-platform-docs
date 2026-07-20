namespace Fitness.Domain.Services;

public static class BmiCalculator
{
    public static decimal Calculate(
        decimal heightCm,
        decimal weightKg)
    {
        if (heightCm <= 0 || weightKg <= 0)
            return 0;

        var heightMeter = heightCm / 100m;

        var bmi = weightKg / (heightMeter * heightMeter);

        return Math.Round(bmi, 2);
    }

    public static string GetStatus(decimal bmi)
    {
        if (bmi == 0)
            return "Unknown";

        if (bmi < 18.5m)
            return "Underweight";

        if (bmi < 25m)
            return "Normal";

        if (bmi < 30m)
            return "Overweight";

        return "Obese";
    }
}