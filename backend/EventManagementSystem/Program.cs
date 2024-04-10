using System.Globalization;
using Asp.Versioning;
using BLL.App;
using BLL.Contracts.App;
using DAL.Contracts.App;
using DAL.EF.App;
using DAL.EF.App.AppDataInit;
using EventManagementSystem.Extensions;
using Microsoft.EntityFrameworkCore;
using PublicApi.v1.DTO.MappingProfiles;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Default connection string not found."), optionsBuilder =>
                       {
                           optionsBuilder.EnableRetryOnFailure();
                           optionsBuilder.CommandTimeout(500);
                       }));

var apiVersioningBuilder = builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
});

apiVersioningBuilder.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IAppUow, AppUow>();
builder.Services.AddScoped<IAppBll, AppBll>();

builder.Services.AddCors(options =>
    options.AddPolicy("CorsAllowAll", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    }));

builder.Services.AddAutoMapper(
    typeof(DAL.DTO.MappingProfiles.AutoMapperProfile),
    typeof(BLL.DTO.MappingProfiles.AutoMapperProfile),
    typeof(AutoMapperProfile));

var supportedCultures = builder.Configuration
    .GetSection("SupportedCultures")
    .GetChildren()
    .Select(x => new CultureInfo(x.Value
                                 ?? throw new InvalidOperationException("Culture not found")))
    .ToArray();

var app = builder.Build();

app.MapControllers();
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("CorsAllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

await SetupAppData(app, app.Environment, app.Configuration);

app.Run();
return;

static async Task SetupAppData(
    IApplicationBuilder app,
    IWebHostEnvironment environment,
    IConfiguration configuration)
{
    using var serviceScope = app.ApplicationServices
        .GetRequiredService<IServiceScopeFactory>()
        .CreateScope();
    var context = serviceScope.ServiceProvider
        .GetService<AppDbContext>() ?? throw new ApplicationException("Problem in services. Can't initialize DB Context");
    var logger = serviceScope.ServiceProvider.GetService<ILogger<IApplicationBuilder>>() ??
        throw new ApplicationException("Problem in services. Can't initialize logger");

    try
    {
        const int retries = 10;
        var delay = TimeSpan.FromSeconds(5);

        for (var i = 0; i < retries; i++)
        {
            logger.LogInformation("Waiting for db connection... {I}/{Retries}", i, retries);
            if (context.Database.CanConnect())
                break;

            Thread.Sleep(delay);
        }
    }
    catch (Exception e)
    {
        logger.LogError(e, "DB can't connect");
        throw;
    }

    if (configuration.GetValue<bool>("AppData:SeedData"))
    {
        logger.LogInformation("Seeding app data");
        await DataInit.SeedAppDataAsync(context);
    }
}

namespace EventManagementSystem
{
    public partial class Program;
}