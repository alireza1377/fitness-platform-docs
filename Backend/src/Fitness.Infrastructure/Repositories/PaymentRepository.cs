using Fitness.Application.Interfaces;
using Fitness.Domain.Entities;
using Fitness.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly FitnessDbContext _context;

    public PaymentRepository(FitnessDbContext context)
    {
        _context = context;
    }

   

    public async Task<Payment?> GetByAuthorityAsync(
        string authority,
        CancellationToken cancellationToken = default)
    {
        return await _context.Payments
            .FirstOrDefaultAsync(
                x => x.Authority == authority,
                cancellationToken);
    }

    public async Task<List<Payment>> GetByUserAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Payments
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(
        Payment payment,
        CancellationToken cancellationToken = default)
    {
        await _context.Payments.AddAsync(
            payment,
            cancellationToken);
    }

    public Task UpdateAsync(
        Payment payment,
        CancellationToken cancellationToken = default)
    {
        _context.Payments.Update(payment);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Payment?> GetByIdAsync(
    Guid paymentId,
    CancellationToken cancellationToken = default)
{
    return await _context.Payments
        .FirstOrDefaultAsync(
            x => x.Id == paymentId,
            cancellationToken);
}
public async Task<List<Payment>> GetUserPaymentsAsync(
    Guid userId,
    CancellationToken cancellationToken = default)
{
    return await _context.Payments
        .Where(x => x.UserId == userId)
        .OrderByDescending(x => x.CreatedAt)
        .ToListAsync(cancellationToken);
}

}