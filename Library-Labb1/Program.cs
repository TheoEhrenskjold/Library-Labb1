using FluentValidation;
using Library_Labb1.Data;
using Library_Labb1.Endpoints;
using Library_Labb1.Repository;
using Microsoft.EntityFrameworkCore;

namespace Library_Labb1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Configure CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin() // Tillåter alla ursprung
                               .AllowAnyMethod() // Tillåter alla HTTP-metoder
                               .AllowAnyHeader(); // Tillåter alla headers
                    });
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddValidatorsFromAssemblyContaining<Program>();

            builder.Services.AddDbContext<LibraryContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionToDB")));
            builder.Services.AddScoped<IBookRepo, BookRepo>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Enable CORS
            app.UseCors("AllowAll");

            app.UseAuthorization();

            app.ConfigurationLibraryEndpoints();

            app.Run();
        }
    }
}
