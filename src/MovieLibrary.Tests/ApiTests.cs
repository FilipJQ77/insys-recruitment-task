using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using MovieLibrary.Api;
using MovieLibrary.Data.Entities;
using MovieLibrary.Data.Entities.Dto;
using Newtonsoft.Json;

namespace MovieLibrary.Tests;

public class Tests
{
    [Test]
    public async Task GetCategories_ReturnsCategories()
    {
        // Arrange
        var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
        var client = server.CreateClient();

        // Act
        var response = await client.GetAsync("/v1/CategoryManagement");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var categories = JsonConvert.DeserializeObject<IEnumerable<Category>>(content);
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(categories.Any(), Is.True);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        });
    }
    
    [Test]
    public async Task CreateCategory_ReturnsCreatedCategory()
    {
        // Arrange
        var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
        var client = server.CreateClient();
        var newCategory = new Category { Name = "New Category" };

        // Act
        var response = await client.PostAsJsonAsync("/v1/CategoryManagement", newCategory);
        response.EnsureSuccessStatusCode();

        var createdCategory = await response.Content.ReadFromJsonAsync<Category>();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(createdCategory, Is.Not.Null);
            Assert.That(createdCategory.Name, Is.EqualTo(newCategory.Name));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        });
    }
    
    [Test]
    public async Task UpdateCategory_ReturnsUpdatedCategory()
    {
        // Arrange
        var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
        var client = server.CreateClient();
        var existingCategoryId = 1;
        var updatedCategory = new Category { Id = existingCategoryId, Name = "Updated Category" };

        // Act
        var response = await client.PutAsJsonAsync($"/v1/CategoryManagement/{existingCategoryId}", updatedCategory);
        response.EnsureSuccessStatusCode();

        var updatedCategoryResponse = await response.Content.ReadFromJsonAsync<Category>();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(updatedCategoryResponse, Is.Not.Null);
            Assert.That(updatedCategoryResponse.Id, Is.EqualTo(existingCategoryId));
            Assert.That(updatedCategoryResponse.Name, Is.EqualTo(updatedCategory.Name));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        });
    }
    
    [Test]
    public async Task DeleteCategory_ReturnsNoContent()
    {
        // Arrange
        var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
        var client = server.CreateClient();
        var categoryIdToDelete = 1;

        // Act
        var response = await client.DeleteAsync($"/v1/CategoryManagement/{categoryIdToDelete}");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
    }
    
    [Test]
    public async Task GetMovies_ReturnsListOfMovies()
    {
        // Arrange
        var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
        var client = server.CreateClient();

        // Act
        var response = await client.GetAsync("/v1/MovieManagement");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var movies = JsonConvert.DeserializeObject<IEnumerable<Movie>>(content);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(movies, Is.Not.Null);
            Assert.That(movies.Any(), Is.True);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        });
    }
    
    [Test]
    public async Task CreateMovie_ReturnsCreatedMovie()
    {
        // Arrange
        var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
        var client = server.CreateClient();
        var newMovie = new Movie { Title = "New Movie", Description = "Description"};

        // Act
        var response = await client.PostAsJsonAsync("/v1/MovieManagement", newMovie);
        response.EnsureSuccessStatusCode();

        var createdMovie = await response.Content.ReadFromJsonAsync<Movie>();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(createdMovie, Is.Not.Null);
            Assert.That(createdMovie.Title, Is.EqualTo(newMovie.Title));
            Assert.That(createdMovie.Description, Is.EqualTo(newMovie.Description));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        });
    }
    
    [Test]
    public async Task UpdateMovie_ReturnsUpdatedMovie()
    {
        // Arrange
        var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
        var client = server.CreateClient();
        var existingMovieId = 1; // ID of the movie to be updated
        var updatedMovie = new Movie { Id = existingMovieId, Title = "Updated Movie", Description = "Desc" };

        // Act
        var response = await client.PutAsJsonAsync($"/v1/MovieManagement/{existingMovieId}", updatedMovie);
        response.EnsureSuccessStatusCode();

        var updatedMovieResponse = await response.Content.ReadFromJsonAsync<Movie>();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(updatedMovieResponse, Is.Not.Null);
            Assert.That(updatedMovieResponse.Id, Is.EqualTo(existingMovieId));
            Assert.That(updatedMovieResponse.Title, Is.EqualTo(updatedMovie.Title));
            Assert.That(updatedMovieResponse.Description, Is.EqualTo(updatedMovie.Description));
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        });
    }
    
    [Test]
    public async Task DeleteMovie_ReturnsNoContent()
    {
        // Arrange
        var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
        var client = server.CreateClient();
        var movieIdToDelete = 1; // ID of the movie to be deleted

        // Act
        var response = await client.DeleteAsync($"/v1/MovieManagement/{movieIdToDelete}");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
    }
    
    [Test]
    public async Task FilterMovies_ReturnsFilteredMovies()
    {
        // Arrange
        var server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
        var client = server.CreateClient();
        var filter = new MovieFilterDto
        {
            Title = "Action",
            Categories = new List<string> { "Action", "Adventure" },
            MinImdbRating = 7.5m,
            MaxImdbRating = 8.5m
        };

        // Act
        var response = await client.PostAsJsonAsync("/v1/MovieManagement/filter", filter);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var filteredMovies = JsonConvert.DeserializeObject<List<Movie>>(content);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(filteredMovies, Is.Not.Null);
            Assert.That(filteredMovies.Any(), Is.True);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        });
    }
}