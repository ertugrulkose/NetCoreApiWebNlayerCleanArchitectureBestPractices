using App.Application.Contracts.Caching;
using App.Application.Extensions;
using App.Caching;
using App.Persistence;
using CleanApp.API.ExceptionHandler;
using CleanApp.API.Extensions;
using CleanApp.API.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithFiltersExt().AddSwaggerGenExt().AddExceptionHandlerExt().AddMemoryCacheExt();

builder.Services.AddRepositories(builder.Configuration).AddServices(builder.Configuration);

var app = builder.Build();

app.UseConfigurePipelineExt();

app.MapControllers();

app.Run();
