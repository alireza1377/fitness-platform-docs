using Fitness.Domain.Entities;
using Fitness.Domain.Enums;
using Fitness.Infrastructure.Database.Context;
using Fitness.Infrastructure.Database.Seed;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Infrastructure.Database;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(FitnessDbContext context)
    {
        await context.Database.MigrateAsync();

        await SubscriptionPlanSeeder.SeedAsync(context);

        if (await context.Categories.AnyAsync())
            return;

        // ==========================
        // Categories
        // ==========================

        var home = new Category(
            "ورزش در خانه",
            "تمرینات بدون تجهیزات",
            "/images/categories/home.jpg",
            null,
            1);

        var dumbbell = new Category(
            "ورزش با دمبل",
            "تمرینات قدرتی",
            "/images/categories/dumbbell.jpg",
            null,
            2);

        var zumba = new Category(
            "زومبا",
            "رقص و هوازی",
            "/images/categories/zumba.jpg",
            null,
            3);

        var yoga = new Category(
            "یوگا",
            "آرامش و انعطاف",
            "/images/categories/yoga.jpg",
            null,
            4);

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
            "۲۰ جلسه تمرینی",
            "/images/programs/home.jpg");

        var dumbbellProgram = new FitnessProgram(
            dumbbell.Id,
            "برنامه دمبل",
            "۱۱ جلسه",
            "/images/programs/dumbbell.jpg");

        var zumbaProgram = new FitnessProgram(
            zumba.Id,
            "زومبا مقدماتی",
            "۱۰ جلسه",
            "/images/programs/zumba.jpg");

        var yogaProgram = new FitnessProgram(
            yoga.Id,
            "یوگا مقدماتی",
            "۱۵ جلسه",
            "/images/programs/yoga.jpg");

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
    fitnessProgramId: program.Id,
    order: i,
    title: $"جلسه {i}",
    videoStorageId: Guid.NewGuid(), // فعلاً موقت
    description: $"ویدئوی شماره {i}",
    downloadUrl: null,
    duration: TimeSpan.FromMinutes(durationMinutes),
    thumbnailUrl: $"https://cdn.test/{folder}/{i}.jpg",
    difficulty: VideoDifficulty.Beginner,
    estimatedCalories: durationMinutes * 5,
    isFree: i == 1,
    isPublished: true,
    hasSubtitle: true,
    hasMultiAudio: true)
                    );
        }
    }
}