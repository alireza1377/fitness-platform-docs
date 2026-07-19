using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Fitness.Infrastructure.Database.Context;

public class FitnessDbContextFactory
    : IDesignTimeDbContextFactory<FitnessDbContext>
{
    public FitnessDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<FitnessDbContext>();

        optionsBuilder.UseNpgsql(
            "Host=localhost;Port=5432;Database=fitness;Username=fitness;Password=123456");

        return new FitnessDbContext(optionsBuilder.Options);
    }
}