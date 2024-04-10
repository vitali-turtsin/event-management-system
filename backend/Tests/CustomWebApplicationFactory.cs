using System.Data.Common;
using DAL.EF.App;
using DAL.EF.App.AppDataInit;
using Domain.App;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Tests;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>
    where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));

            if (descriptor != null) services.Remove(descriptor);

            var dbConnectionDescriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbConnection));

            if (dbConnectionDescriptor != null) services.Remove(dbConnectionDescriptor);

            services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("test_db"));

            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var scopedServices = scope.ServiceProvider;
            using var db = scopedServices.GetRequiredService<AppDbContext>();
            db.Database.EnsureCreated();

            SeedDataAsync(db).Wait();
        });
    }

    private static async Task SeedDataAsync(AppDbContext ctx)
    {
        var paymentMethodId1 = Guid.NewGuid();
        var paymentMethodId2 = Guid.NewGuid();

        ctx.PaymentMethods.AddRange(new List<PaymentMethod>
        {
            new() { Id = paymentMethodId1, Name = "Kaardimakse" },
            new() { Id = paymentMethodId2, Name = "Sularaha" }
        });

        var eventId = Guid.NewGuid();

        ctx.Events.Add(new Event
        {
            Id = eventId,
            Name = "Test Event",
            Description = "Test Description",
            DateTime = DateTime.UtcNow + TimeSpan.FromDays(1),
            Location = "Test Location",
        });

        ctx.People.Add(new Person
        {
            Id = Guid.NewGuid(),
            FirstName = "Test",
            LastName = "Person",
            PersonalCode = "39906170252",
            EventId = eventId,
            PaymentMethodId = paymentMethodId1,
        });

        ctx.People.Add(new Person
        {
            Id = Guid.NewGuid(),
            FirstName = "Test",
            LastName = "Person 2",
            PersonalCode = "39906170252",
            EventId = eventId,
            PaymentMethodId = paymentMethodId2,
        });

        ctx.Organizations.Add(new Organization
        {
            Id = Guid.NewGuid(),
            Name = "Test Organization",
            RegistrationNumber = "123456789",
            NumberOfParticipants = 2,
            EventId = eventId,
            PaymentMethodId = paymentMethodId1,
        });

        await ctx.SaveChangesAsync();
    }
}