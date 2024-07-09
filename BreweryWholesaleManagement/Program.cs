using BreweryWholesaleManagement.Data;
using BreweryWholesaleManagement.Mappers;
using BreweryWholesaleManagement.Middlewares;
using BreweryWholesaleManagement.Repositories;
using BreweryWholesaleManagement.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddDbContext<BreweryContext>(options =>
                   options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddScoped<IBeerMapper, BeerMapper>();
        builder.Services.AddScoped<IWholesalerStockMapper, WholesalerStockMapper>();

        builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        builder.Services.AddTransient<IBrewerRepository, BrewerRepository>();
        builder.Services.AddTransient<IWholesalerRepository, WholesalerRepository>();

        builder.Services.AddTransient<IBrewerService, BrewerService>();
        builder.Services.AddTransient<IQuoteService, QuoteService>();
        builder.Services.AddTransient<IWholesalerService, WholesalerService>();

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Brewery and wholesale management API",
                Description = "Management system for breweries and wholesalers. ",

            });
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        var app = builder.Build();
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            try
            {
                SeedData.Initialize(services);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred seeding the DB.");
            }
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseMiddleware<ErrorHandlingMiddleware>();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}