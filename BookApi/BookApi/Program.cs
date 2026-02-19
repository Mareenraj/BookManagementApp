using BookApi.Extensions;
using BookApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Controllers + validation
builder.Services.AddControllers();

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS for Angular dev server
builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularDev", policy =>
        policy.WithOrigins("*")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

// DI registrations
builder.Services.AddApplicationServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable CORS before Authorization/Controllers
app.UseCors("AngularDev");

app.MapControllers();

app.Run();