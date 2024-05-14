var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

app.MapGet("/calculate/{operation}/{value1}/{value2}", (string operation, double value1, double value2) =>
{
    double result = operation.ToLower() switch
    {
        "soma" => value1 + value2,
        "subtracao" => value1 - value2,
        "multiplicacao" => value1 * value2,
        "divisao" => value2 != 0 ? value1 / value2 : double.NaN,
        _ => double.NaN
    };

    if (double.IsNaN(result))
    {
        return Results.BadRequest("Operação inválida ou divisão por zero.");
    }

    return Results.Ok(result);
});

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}