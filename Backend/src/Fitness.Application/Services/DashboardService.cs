using Fitness.Application.DTOs.Dashboard;
using Fitness.Application.Interfaces;

namespace Fitness.Application.Services;

public class DashboardService : IDashboardService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserProgramProgressRepository _programProgressRepository;
    private readonly IUserVideoProgressRepository _videoProgressRepository;
    private readonly IFitnessProgramRepository _programRepository;

    public DashboardService(
        IUserRepository userRepository,
        IUserProgramProgressRepository programProgressRepository,
        IUserVideoProgressRepository videoProgressRepository,
        IFitnessProgramRepository programRepository)
    {
        _userRepository = userRepository;
        _programProgressRepository = programProgressRepository;
        _videoProgressRepository = videoProgressRepository;
        _programRepository = programRepository;
    }

    public async Task<DashboardDto> GetDashboardAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        // مرحله بعد اینجا پیاده می‌شود

        return new DashboardDto();
    }
}