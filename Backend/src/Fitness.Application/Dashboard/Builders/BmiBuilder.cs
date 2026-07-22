using Fitness.Application.DTOs.Dashboard;
using Fitness.Application.Interfaces;

namespace Fitness.Application.Dashboard.Builders;

public class BmiBuilder
{
    private readonly IUserRepository _userRepository;

    public BmiBuilder(
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<BmiCardDto?> BuildAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        var user =
            await _userRepository.GetByIdAsync(
                userId,
                cancellationToken);

        if (user is null)
            return null;

        if (!user.Height.HasValue ||
            !user.Weight.HasValue ||
            user.Height.Value <= 0)
            return null;

        double height =
            (double)user.Height.Value / 100.0;

        double weight =
            (double)user.Weight.Value;

        double bmi =
            weight / (height * height);

        return new BmiCardDto
        {
            Value = Math.Round(bmi, 1),
            Status = GetStatus(bmi)
        };
    }

    private static string GetStatus(double bmi)
    {
        if (bmi < 18.5)
            return "Underweight";

        if (bmi < 25)
            return "Normal";

        if (bmi < 30)
            return "Overweight";

        return "Obese";
    }
}