using ImageProService.Application.Services;
using ImageProService.Domain.Interfaces;
using ImageProService.Infrastructure.Configurations;
using ImageProService.Infrastructure.Data;
using ImageProService.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
builder.Host.UseSerilog();

// Add services to the container.
builder.Services.Configure<BlobSettings>(builder.Configuration.GetSection("BlobSettings"));
builder.Services.Configure<HuggingFaceSettings>(builder.Configuration.GetSection("HuggingFaceSettings"));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString));


// Register services with DI
builder.Services.AddScoped<IImageRepository, EfImageRepository>();
builder.Services.AddScoped<IBlobStorageService, BlobStorageService>();
builder.Services.AddScoped<IImageProcessingService, ImageProcessingService>();
builder.Services.AddScoped<IAIVisionService, AIVisionService>();
builder.Services.AddScoped<IImageService, ImageService>();


builder.Services.AddHttpClient<IAIVisionService, AIVisionService>();

builder.Services.AddHostedService<ImageAnalysisBackgroundService>();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
