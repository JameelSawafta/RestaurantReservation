using Asp.Versioning;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestaurantReservation.API.Controllers;
using RestaurantReservation.Db.DbContext;
using RestaurantReservation.Db.Repositories;
using RestaurantReservation.Domain.Interfaces.Repositories;
using RestaurantReservation.Domain.Interfaces.Services;
using RestaurantReservation.Domain.Profiles;
using RestaurantReservation.Domain.Services;


namespace RestaurantReservation.CompositionRoot;

class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        string basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..");
        basePath = Path.GetFullPath(basePath);
        builder.Configuration
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true);
            
        
        // Add services to the container.
        builder.Services.AddAuthorization();

        builder.Services.AddControllers().AddApplicationPart(typeof(CustomersController).Assembly);;
        
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<RestaurantReservationDbContext>(
            dbContext => dbContext.UseNpgsql(builder.Configuration["ConnectionStrings:constr"]));
        
        builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
        builder.Services.AddScoped<ICustomerService, CustomerService>();
        
        builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();
        builder.Services.AddScoped<IRestaurantService, RestaurantService>();
        
        builder.Services.AddScoped<ITableRepository, TableRepository>();
        builder.Services.AddScoped<ITableService, TableService>();

        builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        builder.Services.AddScoped<IEmployeeService, EmployeeService>();
        
        // builder.Services.AddAutoMapper(
        //     typeof(CustomerMappingProfile).Assembly
        //     );
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        builder.Services.AddApiVersioning(
            setupAction =>
            {
                setupAction.ReportApiVersions = true;
                setupAction.AssumeDefaultVersionWhenUnspecified = true;
                setupAction.DefaultApiVersion =
                    ApiVersion.Default; 
                setupAction.ApiVersionReader = new HeaderApiVersionReader("x-version");
            }
        );
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        
        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.Run();
    }
}