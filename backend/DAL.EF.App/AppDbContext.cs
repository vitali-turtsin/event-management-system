using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Event> Events { get; set; } = default!;
    public DbSet<Organization> Organizations { get; set; } = default!;
    public DbSet<Person> People { get; set; } = default!;
    public DbSet<PaymentMethod> PaymentMethods { get; set; } = default!;
}