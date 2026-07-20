using Fitness.Domain.Entities;

namespace Fitness.Application.Interfaces;

public interface IRefreshTokenRepository
{
    Task AddAsync(
        RefreshToken refreshToken,
        CancellationToken cancellationToken = default);

    Task<RefreshToken?> GetByTokenHashAsync(
        string tokenHash,
        CancellationToken cancellationToken = default);

   

    Task SaveChangesAsync(
        CancellationToken cancellationToken = default);
        
       
void Update(
    RefreshToken refreshToken);
}