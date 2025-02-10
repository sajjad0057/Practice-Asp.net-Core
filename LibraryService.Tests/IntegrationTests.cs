using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using LibraryService.WebAPI;
using LibraryService.WebAPI.Data;
using LibraryService.WebAPI.DTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using Xunit;

namespace LibraryService.Tests
{
    public class IntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly LibraryContext context;

        public HttpClient Client { get; private set; }

        public IntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            context = new LibraryContext(new DbContextOptionsBuilder<LibraryContext>()
                        .UseSqlite("DataSource=:memory:")
                        .EnableSensitiveDataLogging()
                        .Options);
            Client = _factory.WithWebHostBuilder(builder =>
                builder.UseStartup<Startup>()
                .ConfigureServices(services =>
                {
                    services.RemoveAll(typeof(LibraryContext));
                    services.AddSingleton(context);

                    context.Database.OpenConnection();
                    context.Database.EnsureCreated();

                    context.SaveChanges();

                    // Clear local context cache
                    foreach (var entity in context.ChangeTracker.Entries().ToList())
                    {
                        entity.State = EntityState.Detached;
                    }
                })
            ).CreateClient();
        }

        private async Task SeedLibrary()
        {
            var libraries = new List<Library>
            {
                new Library { Name = "Library Name 1", Location = "Location 1" },
                new Library { Name = "Library Name 2", Location = "Location 2" },
                new Library { Name = "Library Name 3", Location = "Location 3" },
                new Library { Name = "Library Name 4", Location = "Location 4" }
            };

            await context.Libraries.AddRangeAsync(libraries);
            await context.SaveChangesAsync();  // Save to the database
        }

        private async Task SeedBook(string bookName, int libraryId)
        {
            var bookForm = new BookForm
            {
                Name = bookName
            };
            var response1 = await Client.PostAsync($"/api/libraries/{libraryId}/books",
                new StringContent(JsonConvert.SerializeObject(bookForm), Encoding.UTF8, "application/json"));
        }

        // TEST NAME - addBookToLibrary
        // TEST DESCRIPTION - It adds book to a library
        [Fact]
        public async Task TestAddBook_Ok_GetBook_NotFound()
        {
            await SeedLibrary();

            var bookForm = new BookForm
            {
                Name = "Test book 1",
            };

            var response1 = await Client.PostAsync($"/api/libraries/1/books",
                new StringContent(JsonConvert.SerializeObject(bookForm), Encoding.UTF8, "application/json"));

            response1.StatusCode.Should().BeEquivalentTo(StatusCodes.Status201Created);

            bookForm = new BookForm
            {
                Name = "Test book 2",
            };

            var response2 = await Client.PostAsync($"/api/libraries/100/books",
                new StringContent(JsonConvert.SerializeObject(bookForm), Encoding.UTF8, "application/json"));

            response2.StatusCode.Should().BeEquivalentTo(StatusCodes.Status404NotFound);
        }

        // TEST NAME - getBooksInALibrary
        // TEST DESCRIPTION - It finds all books in a library by ID
        [Fact]
        public async Task TestGetBooks_Ok_NotFound()
        {
            await SeedLibrary();

            await SeedBook("test book 1", 1);
            await SeedBook("test book 2", 1);

            var response1 = await Client.GetAsync($"/api/libraries/2/books");
            response1.StatusCode.Should().BeEquivalentTo(StatusCodes.Status200OK);
            var books = JsonConvert.DeserializeObject<IEnumerable<Book>>(response1.Content.ReadAsStringAsync().Result).ToList();
            books.Count.Should().Be(0);
            
            var response2 = await Client.GetAsync($"/api/libraries/1/books");
            response2.StatusCode.Should().BeEquivalentTo(StatusCodes.Status200OK);
            var books2 = JsonConvert.DeserializeObject<IEnumerable<Book>>(response2.Content.ReadAsStringAsync().Result).ToList();
            books2.Count.Should().Be(2);

            var response3 = await Client.GetAsync($"/api/libraries/31232/books");
            response3.StatusCode.Should().BeEquivalentTo(StatusCodes.Status404NotFound);
        }

        // TEST NAME - deleteLibraryById
        // TEST DESCRIPTION - Check delete library web api end point
        [Fact]
        public async Task TestDeleteLibrary()
        {
            await SeedLibrary();

            var bookForm = new BookForm
            {
                Name = "test book 1",
            };

            // add book to library
            var response0 = await Client.PostAsync("/api/libraries/1/books",
                new StringContent(JsonConvert.SerializeObject(bookForm), Encoding.UTF8, "application/json"));
            response0.StatusCode.Should().BeEquivalentTo(StatusCodes.Status201Created);

            // delete library
            var response1 = await Client.DeleteAsync("/api/libraries/1");
            response1.StatusCode.Should().BeEquivalentTo(StatusCodes.Status204NoContent);

            // Verify that delete is successful
            var response2 = await Client.GetAsync("/api/libraries/1/books");
            response2.StatusCode.Should().BeEquivalentTo(StatusCodes.Status404NotFound);

            var response3 = await Client.DeleteAsync("/api/libraries/1");
            response3.StatusCode.Should().BeEquivalentTo(StatusCodes.Status404NotFound);
        }
    }
}
