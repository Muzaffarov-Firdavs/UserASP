using ClassWork.Data.DbContexts;
using ClassWork.Service.Helpers;
using ClassWork.Service.Mappers;
using ClassWork.Web.Extensions;
using ClassWork.Web.Helpers;
using ClassWork.Web.Middlewares;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// configuration join
builder.Services.AddDbContext<AppDbContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// mapper join
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<EmailVerification>();
builder.Services.AddCustomServices();

// swagger join and set up
builder.Services.AddSwaggerService();

// JWT services
builder.Services.AddJwtService(builder.Configuration);

// Logger
var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddControllers(options =>
    options.Conventions.Add(
        new RouteTokenTransformerConvention(new RouteConfiguration())));


var app = builder.Build();

EnvironmentHelper.WebHostPath = app.Services.GetRequiredService<IWebHostEnvironment>().WebRootPath;

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
