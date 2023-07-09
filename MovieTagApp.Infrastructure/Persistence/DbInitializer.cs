using Microsoft.EntityFrameworkCore;

namespace MovieTagApp.Infrastructure.Persistence;
public class DbInitializer
{
    private readonly MovieTagAppContext _context;

    public DbInitializer(MovieTagAppContext context)
    {
        _context = context;
    }

    public async Task InitializeAsync()
    {
        await _context.Database.MigrateAsync();
    }
}
