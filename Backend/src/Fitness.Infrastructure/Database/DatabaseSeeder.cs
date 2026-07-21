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

        context.Categories.AddRange(home, dumbbell, zumba, yoga);

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

        AddVideos(context, homeProgram, 20, 25, "home");
        AddVideos(context, dumbbellProgram, 11, 30, "dumbbell");
        AddVideos(context, zumbaProgram, 10, 35, "zumba");
        AddVideos(context, yogaProgram, 15, 40, "yoga");

        homeProgram.UpdateVideoCount(20);
        dumbbellProgram.UpdateVideoCount(11);
        zumbaProgram.UpdateVideoCount(10);
        yogaProgram.UpdateVideoCount(15);

        await context.SaveChangesAsync();
    }

    private static void AddVideos(
        FitnessDbContext context,
        FitnessProgram program,
        int count,
        int durationMinutes,
        string folder)
    {
        for (int i = 1; i <= count; i++)
        {
            context.ProgramVideos.Add(
                new ProgramVideo(
                    program.Id,
                    i,
                    $"جلسه {i}",
                    $"https://cdn.test/{folder}/{i}.mp4",
                    TimeSpan.FromMinutes(durationMinutes)));
        }
    }
}