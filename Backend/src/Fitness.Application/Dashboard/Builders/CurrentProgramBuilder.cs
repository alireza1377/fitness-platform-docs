using Fitness.Application.DTOs.Dashboard;
using Fitness.Application.Interfaces;

namespace Fitness.Application.Dashboard.Builders;

public class CurrentProgramBuilder
{
    private readonly IUserProgramProgressRepository _programProgressRepository;

    public CurrentProgramBuilder(
        IUserProgramProgressRepository programProgressRepository)
    {
        _programProgressRepository = programProgressRepository;
    }

    public async Task<CurrentProgramCardDto?> BuildAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        var currentProgram =
            await _programProgressRepository.GetCurrentProgramAsync(
                userId,
                cancellationToken);

        if (currentProgram is null)
            return null;

        return new CurrentProgramCardDto
        {
            ProgramId = currentProgram.FitnessProgramId,
            Title = currentProgram.FitnessProgram.Title,
            Progress = currentProgram.Percentage,
            CompletedVideos = currentProgram.CompletedVideos,
            TotalVideos = currentProgram.TotalVideos
        };
    }
}