using Fitness.Application.DTOs.Dashboard;

namespace Fitness.Application.Dashboard.Builders;

public class QuickActionBuilder
{
    public List<QuickActionDto> Build()
    {
        return
        [
            new()
            {
                Title = "شروع تمرین",
                Icon = "play",
                Route = "/workout"
            },

            new()
            {
                Title = "برنامه‌ها",
                Icon = "program",
                Route = "/programs"
            },

            new()
            {
                Title = "پیشرفت من",
                Icon = "chart",
                Route = "/progress"
            },

            new()
            {
                Title = "پروفایل",
                Icon = "profile",
                Route = "/profile"
            }
        ];
    }
}