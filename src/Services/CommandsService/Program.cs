using CommandsService.Apis;
using CommandsService.Persistence;
using CommandsService.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services
    .AddDbContext<AppDbContext>(options =>  options.UseInMemoryDatabase("InMenCommand"));

builder.Services.AddScoped<ICommandRepo, CommandRepo>();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
}

app.UseSwagger();
app.UseSwaggerUI();

app.MapPlatformEndpoints("api/c");
app.MapCommandEndpoints("api/c");

app.MapPost("c/platforms", () =>
{
    Console.WriteLine("======= Service is up and running");
    return Results.Ok(new { message = "OK" });
});





app.UseHttpsRedirection();

app.Run();