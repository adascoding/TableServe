using FluentMigrator.Runner;
using System.Reflection;

namespace TableServe.API.Extensions
{
    public static class MigrationExtensions
    {
        public static IServiceCollection AddFluentMigrator(this IServiceCollection services, string connectionString)
        {
            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSQLite()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole());

            return services;
        }

        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                runner.MigrateUp();
            }
        }
    }
}
