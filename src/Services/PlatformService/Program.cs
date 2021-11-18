using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using PlatformService.Application.SyncDataServices.Http;
using PlatformService.Persistence;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options => { options.UseInMemoryDatabase("InMen");options.LogTo(Console.WriteLine, LogLevel.Information); });
builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();
builder.Services.AddControllers()
    .AddFluentValidation(x => { 
        x.RegisterValidatorsFromAssemblyContaining<Program>();
    });
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "PlatformService", Version = "v1" });
});

builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();

Console.WriteLine($"---> Command Service Endpoint {builder.Configuration["CommandService"]}");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   
}

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PlatformService v1"));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandler(e => e.Run(async context =>
{
    var exception = context.Features.Get<IExceptionHandlerPathFeature>()?.Error;
    if (exception != null) 
    {
        var response = new {
            title = exception.Message,
            errors = new { exception.Message, exception.StackTrace, exception.InnerException, exception.HResult},
            path = context.Request.Path.Value,
            source = exception.Source,
            timeStamp = DateTime.Now
        };
        await context.Response.WriteAsJsonAsync(response);
    }
}));

PrepDb.PrepPopulation(app);

app.Run();


