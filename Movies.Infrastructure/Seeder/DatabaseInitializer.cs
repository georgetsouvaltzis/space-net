using Microsoft.Extensions.DependencyInjection;
using Movies.Domain.Entities;
using Movies.Infrastructure.Db;

namespace Movies.Infrastructure.Seeder
{
    public static class DatabaseInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<MoviesDbContext>();

            if (!db.Database.EnsureCreated()) return;

            await db.Users.AddAsync(new User
            {
                Id = 1,
                FirstName = "George",
                LastName = "Tsouvaltzis",
                WatchList = new()
            });

            await db.SaveChangesAsync();
        }
    }
}
