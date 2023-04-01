using AtisazBazar.DataAccess.Contexts;
using AtisazBazar.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace AtisazBazar.DataAccess
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            //register data layer
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer( connectionString));

            //register repositories
            services.AddScoped<ITodoRepository, TodoRepository>();
        }

        public static void AddDbInitialazer(this IServiceProvider services)
        {
            // migrate any database changes on startup (includes initial db creation)
            using (var scope = services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                context.Database.Migrate();
            }
        }
    }
}
