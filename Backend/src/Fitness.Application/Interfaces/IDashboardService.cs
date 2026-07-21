using Fitness.Application.DTOs.Dashboard;

namespace Fitness.Application.Interfaces;

public interface IDashboardService
{
    Task<DashboardDto> GetDashboardAsync(
        Guid userId,
        CancellationToken cancellationToken = default);
}