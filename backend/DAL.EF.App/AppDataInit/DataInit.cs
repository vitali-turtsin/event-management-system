using Domain.App;

namespace DAL.EF.App.AppDataInit;

public static class DataInit
{
    public static async Task SeedAppDataAsync(AppDbContext ctx)
    {
        var paymentMethodId1 = Guid.NewGuid();
        var paymentMethodId2 = Guid.NewGuid();

        ctx.PaymentMethods.AddRange(new List<PaymentMethod>
        {
            new() { Id = paymentMethodId1, Name = "Kaardimakse" },
            new() { Id = paymentMethodId2, Name = "Sularaha" }
        });

        var eventId1 = Guid.NewGuid();
        var eventId2 = Guid.NewGuid();

        ctx.Events.AddRange(new List<Event>
        {
            new() { Id = eventId1, Name = "Test Event 1", Description = "Test Event 1 Description",
                DateTime = DateTime.UtcNow + TimeSpan.FromDays(1), Location = "Test Event 1 Location" },
            new() { Id = eventId2, Name = "Test Event 2", Description = "Test Event 2 Description",
                DateTime = DateTime.UtcNow - TimeSpan.FromDays(1), Location = "Test Event 2 Location"}
        });

        ctx.People.AddRange(new List<Person>
        {
            new() { EventId = eventId1, FirstName = "John", LastName = "Doe", PersonalCode = "39906170252", Description = "Test Person 1", PaymentMethodId = paymentMethodId1 },
            new() { EventId = eventId2, FirstName = "Jane", LastName = "Doe", PersonalCode = "39906170252", Description = "Test Person 2", PaymentMethodId = paymentMethodId2 },
        });

        ctx.Organizations.AddRange(new List<Organization>
        {
            new() { EventId = eventId2, Name = "Test Organization 1", Description = "Test Organization 1 Description", RegistrationNumber = "49624097", NumberOfParticipants = 50, PaymentMethodId = paymentMethodId1 },
        });

        await ctx.SaveChangesAsync();
    }
}