using Fitness.Domain.Entities;

namespace Fitness.Application.Interfaces;

public interface IPaymentRepository
{
    Task<Payment?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<Payment?> GetByAuthorityAsync(
        string authority,
        CancellationToken cancellationToken = default);

    Task<List<Payment>> GetByUserAsync(
        Guid userId,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        Payment payment,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        Payment payment,
        CancellationToken cancellationToken = default);

    Task SaveChangesAsync(
        CancellationToken cancellationToken = default);
}