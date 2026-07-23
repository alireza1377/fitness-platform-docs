using Fitness.Domain.Entities;
using Fitness.Domain.Enums;
using Fitness.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Database.Seed;

public static class SubscriptionPlanSeeder
{
    public static async Task SeedAsync(
        FitnessDbContext context)
    {
        if (await context.SubscriptionPlans.AnyAsync())
            return;

        var plans = new List<SubscriptionPlan>
        {
            new SubscriptionPlan(
                "Premium Monthly",
                "30 Days Access",
                490000,
                SubscriptionType.PremiumMonthly,
                30,
                1),

            new SubscriptionPlan(
                "Premium Quarterly",
                "90 Days Access",
                1290000,
                SubscriptionType.PremiumQuarterly,
                90,
                2),

            new SubscriptionPlan(
                "Premium Yearly",
                "365 Days Access",
                4490000,
                SubscriptionType.PremiumYearly,
                365,
                3)
        };

        await context.SubscriptionPlans.AddRangeAsync(plans);

        await context.SaveChangesAsync();
    }
}