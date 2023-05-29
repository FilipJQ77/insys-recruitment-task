using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MovieLibrary.Core.Category.Commands;
using MovieLibrary.Core.Category.Handlers;
using MovieLibrary.Core.Category.Queries;
using MovieLibrary.Core.Movie.Commands;
using MovieLibrary.Core.Movie.Handlers;
using MovieLibrary.Core.Movie.Queries;
using MovieLibrary.Data;
using MovieLibrary.Data.Entities;
using MovieLibrary.Data.Repository;
using MovieLibrary.Data.Repository.MovieRepository;

namespace MovieLibrary.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFrameworkSqlite().AddDbContext<MovieLibraryContext>();

            services.AddScoped<DbContext, MovieLibraryContext>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IMovieRepository, MovieRepository>();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Startup>());

            services.AddScoped<IRequestHandler<DeleteCategory, bool>, DeleteCategoryHandler>();
            services.AddScoped<IRequestHandler<GetCategories, IEnumerable<Category>>, GetCategoriesHandler>();
            services.AddScoped<IRequestHandler<GetCategory, Category>, GetCategoryHandler>();
            services.AddScoped<IRequestHandler<PostCategory, Category>, PostCategoryHandler>();
            services.AddScoped<IRequestHandler<PutCategory, Category>, PutCategoryHandler>();
            
            services.AddScoped<IRequestHandler<DeleteMovie, bool>, DeleteMovieHandler>();
            services.AddScoped<IRequestHandler<GetMovies, IEnumerable<Movie>>, GetMoviesHandler>();
            services.AddScoped<IRequestHandler<GetFilteredMovies, IEnumerable<Movie>>, GetFilteredMoviesHandler>();
            services.AddScoped<IRequestHandler<GetMovie, Movie>, GetMovieHandler>();
            services.AddScoped<IRequestHandler<PostMovie, Movie>, PostMovieHandler>();
            services.AddScoped<IRequestHandler<PutMovie, Movie>, PutMovieHandler>();
            
            services.AddControllers();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Movie library API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Movie library API");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
