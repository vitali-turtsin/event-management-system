using Microsoft.AspNetCore.Mvc.Testing;
using PublicApi.v1.DTO;
using System.Text;
using System.Text.Json;

namespace Tests.api
{
    public class PeopleControllerTests(CustomWebApplicationFactory<EventManagementSystem.Program> factory) :
    IClassFixture<CustomWebApplicationFactory<EventManagementSystem.Program>>
    {
        private readonly HttpClient _client = factory
            .CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });

        private readonly CustomWebApplicationFactory<EventManagementSystem.Program> _factory = factory;

        private readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        private const string Url = "/api/v1/People";

        [Fact]
        public async Task GetPeople()
        {
            var response = await _client.GetAsync(Url);
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var people = JsonSerializer.Deserialize<IEnumerable<Person>>(stringResponse, _jsonSerializerOptions);
            Assert.NotNull(people);
            Assert.NotEmpty(people);
            Assert.Contains(people, e => e.FirstName == "Test");
        }

        [Fact]
        public async Task GetPerson()
        {
            var people = JsonSerializer.Deserialize<IEnumerable<Person>>(
                await (await _client.GetAsync(Url)).Content.ReadAsStringAsync(), _jsonSerializerOptions);

            Assert.NotNull(people);
            Assert.NotEmpty(people);

            var response = await _client.GetAsync($"{Url}/{people.First().Id}");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var person = JsonSerializer.Deserialize<Person>(stringResponse, _jsonSerializerOptions);

            Assert.NotNull(person);
            Assert.Equal("Test", person.FirstName);
            Assert.NotNull(person.PaymentMethod);
        }

        [Fact]
        public async Task PostPerson()
        {
            var @event = JsonSerializer.Deserialize<IEnumerable<Event>>(
                                              await (await _client.GetAsync("/api/v1/Events")).Content.ReadAsStringAsync(), _jsonSerializerOptions)!.First();
            var paymentMethods = JsonSerializer.Deserialize<IEnumerable<PaymentMethod>>(
                               await (await _client.GetAsync("/api/v1/PaymentMethods")).Content.ReadAsStringAsync(), _jsonSerializerOptions);

            Assert.NotNull(paymentMethods);

            var person = new Person
            {
                FirstName = "New",
                LastName = "Person",
                PersonalCode = "39906170252",
                EventId = @event.Id,
                PaymentMethodId = paymentMethods.First().Id
            };

            var response = await _client.PostAsync(Url, new StringContent(
                               JsonSerializer.Serialize(person), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var addedPerson = JsonSerializer.Deserialize<Person>(stringResponse, _jsonSerializerOptions);

            Assert.NotNull(addedPerson);
            Assert.Equal("New", addedPerson.FirstName);
        }

        [Fact]
        public async Task PutPerson()
        {
            var people = JsonSerializer.Deserialize<IEnumerable<Person>>(
                               await (await _client.GetAsync(Url)).Content.ReadAsStringAsync(), _jsonSerializerOptions);

            Assert.NotNull(people);

            var person = people.First();
            person.FirstName = "Updated Name";

            var response = await _client.PutAsync(Url + "/" + person.Id, new StringContent(
                                              JsonSerializer.Serialize(person), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();

            var updatedPerson = JsonSerializer.Deserialize<Person>(
                               await (await _client.GetAsync($"{Url}/{person.Id}")).Content.ReadAsStringAsync(), _jsonSerializerOptions);

            Assert.NotNull(updatedPerson);
            Assert.Equal("Updated Name", updatedPerson.FirstName);
        }

        [Fact]
        public async Task DeletePerson()
        {
            var people = JsonSerializer.Deserialize<IEnumerable<Person>>(
                                              await (await _client.GetAsync(Url)).Content.ReadAsStringAsync(), _jsonSerializerOptions);

            Assert.NotNull(people);

            var response = await _client.DeleteAsync($"{Url}/{people.First().Id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
