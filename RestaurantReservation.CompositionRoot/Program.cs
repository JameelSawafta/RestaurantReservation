using System.Text;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
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

        // remove these lines
        // move this class to the API project
        
        // string basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..");
        // basePath = Path.GetFullPath(Directory.GetCurrentDirectory());
        // builder.Configuration
        //     .SetBasePath(basePath)
        //     .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        //     .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true);
           
        
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Authentication:Issuer"], 
                    ValidAudience = builder.Configuration["Authentication:Audience"], 
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
                };
            });

        
        // Add services to the container.
        builder.Services.AddAuthorization();

        builder.Services.AddControllers().AddApplicationPart(typeof(CustomersController).Assembly);;
        
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your token in the text input below.<br> Example: 'Bearer 12345abcdef'"
                });

                options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            }
            );

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
        
        builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();
        builder.Services.AddScoped<IMenuItemService, MenuItemService>();
        
        builder.Services.AddScoped<IOrderRepository, OrderRepository>();
        builder.Services.AddScoped<IOrderService, OrderService>();
        
        builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        builder.Services.AddScoped<IOrderItemService, OrderItemService>();
        
        builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
        builder.Services.AddScoped<IReservationService, ReservationService>();
        
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

        app.UseAuthentication();
        
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.Run();
    }
}