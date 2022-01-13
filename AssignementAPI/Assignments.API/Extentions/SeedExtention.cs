using Assignments.DAL.Context;
using Assignments.DAL.Seeds;
using Microsoft.EntityFrameworkCore;

namespace Assignments.API.Extentions
{
    public static class SeedExtention
    {
        public static IHost SeedData(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<AssignmentContext>();

            context.Database.Migrate();

            new UserSeeder(context).SeedData();

            return host;
        }
    }
}
