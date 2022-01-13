using Assignments.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Assignments.API.Extentions
{
    public static class DatabaseExtention
    {
        private static readonly int Timeout = 120;
        public static void ConfigureDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AssignmentContext>(options =>
            {
                options.UseLazyLoadingProxies()
                .UseSqlServer(configuration.GetConnectionString(nameof(AssignmentContext)), sqlServerOptions =>
                {
                    sqlServerOptions.CommandTimeout(Timeout);
                    sqlServerOptions.MigrationsAssembly("AssignmentAPI");
                });

                options.ConfigureWarnings(warnings =>
                {
                    warnings.Log(RelationalEventId.TransactionError);
                    //warnings.Log(RelationalEventId.QueryClientEvaluationWarning);
                    //warnings.Log(RelationalEventId.QueryPossibleExceptionWithAggregateOperator);
                    warnings.Log(RelationalEventId.QueryPossibleUnintendedUseOfEqualsWarning);
                });
            });

            services.AddDatabaseDeveloperPageExceptionFilter();
        }
    }
}
