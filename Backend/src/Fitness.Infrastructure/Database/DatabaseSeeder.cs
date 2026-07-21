using Fitness.Domain.Entities;
using Fitness.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Database;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(FitnessDbContext context)
    {
        await context.Database.MigrateAsync();

        if (await context.Categories.AnyAsync())
            return;

        // ==========================
        // Categories
        // ==========================

        var home = new Category(
            "ورزش در خانه",
            "تمرینات بدون تجهیزات");

        var dumbbell = new Category(
            "ورزش با دمبل",
            "تمرینات قدرتی");

        var zumba = new Category(
            "زومبا",
            "رقص و هوازی");

        var yoga = new Category(
            "یوگا",
            "آرامش و انعطاف");

        context.Categories.AddRange(
            home,
            dumbbell,
            zumba,
            yoga);

        await context.SaveChangesAsync();

        // ==========================
        // Programs
        // ==========================

        var homeProgram = new FitnessProgram(
            home.Id,
            "برنامه ورزش در خانه",
            "۲۰ جلسه تمرینی");

        var dumbbellProgram = new FitnessProgram(
            dumbbell.Id,
            "برنامه دمبل",
            "۱۱ جلسه");

        var zumbaProgram = new FitnessProgram(
            zumba.Id,
            "زومبا مقدماتی",
            "۱۰ جلسه");

        var yogaProgram = new FitnessProgram(
            yoga.Id,
            "یوگا مقدماتی",
            "۱۵ جلسه");

        context.FitnessPrograms.AddRange(
            homeProgram,
            dumbbellProgram,
            zumbaProgram,
            yogaProgram);

        await context.SaveChangesAsync();

                // ==========================
        // Videos
        // ==========================

        for (int i = 1; i <= 20; i++)
        {
            context.ProgramVideos.Add(
                new ProgramVideo(
                    homeProgram.Id,
                    i,
                    $"جلسه {i}",
                    $"https://cdn.test/home/{i}.mp4",
                    TimeSpan.FromMinutes(25)));
        }

        for (int i = 1; i <= 11; i++)
        {
            context.ProgramVideos.Add(
                new ProgramVideo(
                    dumbbellProgram.Id,
                    i,
                    $"جلسه {i}",
                    $"https://cdn.test/dumbbell/{i}.mp4",
                    TimeSpan.FromMinutes(30)));
        }

        for (int i = 1; i <= 10; i++)
        {
            context.ProgramVideos.Add(
                new ProgramVideo(
                    zumbaProgram.Id,
                    i,
                    $"جلسه {i}",
                    $"https://cdn.test/zumba/{i}.mp4",
                    TimeSpan.FromMinutes(35)));
        }

        for (int i = 1; i <= 15; i++)
        {
            context.ProgramVideos.Add(
                new ProgramVideo(
                    yogaProgram.Id,
                    i,
                    $"جلسه {i}",
                    $"https://cdn.test/yoga/{i}.mp4",
                    TimeSpan.FromMinutes(40)));
        }

        homeProgram.UpdateVideoCount(20);
        dumbbellProgram.UpdateVideoCount(11);
        zumbaProgram.UpdateVideoCount(10);
        yogaProgram.UpdateVideoCount(15);

        await context.SaveChangesAsync();
    }
}