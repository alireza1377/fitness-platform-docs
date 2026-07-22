using Fitness.Domain.Entities;

namespace Fitness.Application.Interfaces;

public interface IPaymentRepository
{
    

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

        Task<Payment?> GetByIdAsync(
    Guid paymentId,
    CancellationToken cancellationToken = default);
}