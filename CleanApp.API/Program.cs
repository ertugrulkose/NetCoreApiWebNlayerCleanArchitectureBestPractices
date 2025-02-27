using App.Application.Contracts.Caching;
using App.Application.Extensions;
using App.Caching;
using App.Persistence;
using CleanApp.API.ExceptionHandler;
using CleanApp.API.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<FluentValidationFilter>();
    // Turn off default nullable reference types control
    // options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddRepositories(builder.Configuration).AddServices(builder.Configuration);
builder.Services.AddScoped(typeof(NotFoundFilter<,>));
// for ExceptionHandler
builder.Services.AddExceptionHandler<CriticalExceptionHandler>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

// for Memory Cache
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<ICacheService,CacheService>();

// For Swagger
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseExceptionHandler(x => { });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
