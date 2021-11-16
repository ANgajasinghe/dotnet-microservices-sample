var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
}

app.UseSwagger();
app.UseSwaggerUI();


app.MapPost("c/platforms", () =>
{
    Console.WriteLine("======= Service is up and running");
    return Results.Ok(new { message = "OK" });
});




app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();