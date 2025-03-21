using App.Repositories.Extensions;
using App.Services;
using App.Services.Extensions;

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

// For Swagger
builder.Services.AddSwaggerGen();

// CORS AYARLARI EKLEND� 

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", builder =>
    {
        builder.WithOrigins("http://localhost:5173") // Frontend'in adresi
            .AllowAnyMethod() // GET, POST, PUT, DELETE her �eye izin ver
            .AllowAnyHeader() // Authorization, Content-Type gibi t�m header'lar� kabul et
            .AllowCredentials(); // E�er JWT veya Cookie tabanl� kimlik do�rulama varsa bunu a�
    });
});


builder.Services.AddRepositories(builder.Configuration).AddServices(builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler(x => {});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    // For Swagger
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// CORS DEVREYE ALINDI 
app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();
