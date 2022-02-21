using HomeMovieLibrary.Api;
using HomeMovieLibrary.Api.Models.DB;
using HomeMovieLibrary.Api.Repositories;
using HomeMovieLibrary.Api.ServiceExtensions;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var sqliteDatSource = builder.Configuration.GetValue<string>("SqliteDataSource");
builder.Services.AddSqlite<MovieLibraryDbContext>($"Data Source={sqliteDatSource}", optionsAction: o =>
{
    if (builder.Environment.IsDevelopment())
    {
        o.EnableSensitiveDataLogging();
    }
});

builder.Services.AddLogging(logBuilder =>
{
    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .CreateLogger();

    logBuilder.ClearProviders().AddSerilog(dispose: true);
});


builder.Services.AddMapster();

builder.Services.AddScoped<GenericCRUDRepository<Movie>>();
builder.Services.AddScoped<GenericCRUDRepository<Author>>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc("v1", new OpenApiInfo { Title = "Home Movie Library API", Version = "v1" });
});

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(config =>
    {
        config.SwaggerEndpoint("v1/swagger.json", "Home Movie Library");
    });
}

app.Run();
