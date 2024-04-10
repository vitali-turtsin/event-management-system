using Microsoft.AspNetCore.Mvc.Testing;
using PublicApi.v1.DTO;
using System.Text;
using System.Text.Json;

namespace Tests.api
{
    public class EventsControllerTests(CustomWebApplicationFactory<EventManagementSystem.Program> factory) :
    IClassFixture<CustomWebApplicationFactory<EventManagementSystem.Program>>
    {
        private readonly HttpClient _client = factory
            .CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });

        private readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        private const string Url = "/api/v1/Events";

        [Fact]
        public async Task GetEvents()
        {
            var response = await _client.GetAsync(Url);
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var events = JsonSerializer.Deserialize<IEnumerable<Event>>(stringResponse, _jsonSerializerOptions);
            Assert.NotNull(events);
            Assert.Contains(events, e => e.Name == "Test Event");

            var people = events.First().People;
            var organizations = events.First().Organizations;

            Assert.NotNull(people);
            Assert.NotEmpty(people);
            Assert.NotNull(people.First().PaymentMethod);

            Assert.NotNull(organizations);
            Assert.NotEmpty(organizations);
            Assert.NotNull(organizations.First().PaymentMethod);
        }

        [Fact]
        public async Task GetEvent()
        {
            var events = JsonSerializer.Deserialize<IEnumerable<Event>>(
                await (await _client.GetAsync(Url)).Content.ReadAsStringAsync(), _jsonSerializerOptions);

            Assert.NotNull(events);

            var response = await _client.GetAsync($"{Url}/{events.First().Id}");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var @event = JsonSerializer.Deserialize<Event>(stringResponse, _jsonSerializerOptions);

            Assert.NotNull(@event);
            Assert.Equal("Test Event", @event.Name);
        }

        [Fact]
        public async Task PostEvent()
        {
            var @event = new Event
            {
                Name = "New Event",
                Description = "New Description",
                DateTime = DateTime.UtcNow + TimeSpan.FromDays(1),
                Location = "New Location",
            };

            var response = await _client.PostAsync(Url, new StringContent(
                               JsonSerializer.Serialize(@event), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var addedEvent = JsonSerializer.Deserialize<Event>(stringResponse, _jsonSerializerOptions);

            Assert.NotNull(addedEvent);
            Assert.Equal("New Event", addedEvent.Name);
        }
    }
}
