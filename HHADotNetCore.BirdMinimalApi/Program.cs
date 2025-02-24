using Newtonsoft.Json;

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

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapGet("/birds", () =>
{
    string folderPath = "Data/birds.json";
    var jsonStr = File.ReadAllText(folderPath);
    var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr)!;
    return Results.Ok(result.Tbl_Bird);
})
.WithName("GetBirds")
.WithOpenApi();

app.MapGet("/birds/{id}", (int id) =>
{
    string folderPath = "Data/birds.json";
    var jsonStr = File.ReadAllText(folderPath);
    var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr)!;

    var item = result.Tbl_Bird.FirstOrDefault(x=> x.Id == id);
    if (item == null) return Results.BadRequest("No data found.");

    return Results.Ok(item);
})
.WithName("GetBird")
.WithOpenApi();

app.MapPost("/birds", (BirdModel bird) =>
{
    string folderPath = "Data/birds.json";
    var jsonStr = File.ReadAllText(folderPath);
    var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr)!;

    var birdList = result.Tbl_Bird.ToList();
    bird.Id = birdList.Count == 0 ? 1 : birdList.Max(x => x.Id) + 1 ;
    birdList.Add(bird);

    result.Tbl_Bird = birdList.ToArray();

    var updatedJsonStr = JsonConvert.SerializeObject(result, Formatting.Indented);
    File.WriteAllText(folderPath, updatedJsonStr);

    return Results.Ok(result.Tbl_Bird);
})
.WithName("CreateBirds")
.WithOpenApi();

app.MapPut("/birds/{id}", (int id, BirdModel bird) =>
{
    string folderPath = "Data/birds.json";
    var jsonStr = File.ReadAllText(folderPath);
    var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr)!;

    var item = result.Tbl_Bird.FirstOrDefault(b => b.Id == id);
    if (item == null)
    {
        return Results.NotFound($"Bird with Id {id} not found.");
    }

    item.BirdMyanmarName = bird.BirdMyanmarName;
    item.BirdEnglishName = bird.BirdEnglishName;
    item.Description = bird.Description;
    item.ImagePath = bird.ImagePath;

    var updatedJsonStr = JsonConvert.SerializeObject(result, Formatting.Indented);
    File.WriteAllText(folderPath, updatedJsonStr);

    return Results.Ok(result.Tbl_Bird);
})
.WithName("UpdateBird")
.WithOpenApi();

app.MapDelete("/birds/{id}", (int id) =>
{
    string folderPath = "Data/birds.json";
    var jsonStr = File.ReadAllText(folderPath);
    var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr)!;

    var bird = result.Tbl_Bird.FirstOrDefault(b => b.Id == id);
    if (bird == null)
    {
        return Results.NotFound($"Bird with Id {id} not found.");
    }

    var birdList = result.Tbl_Bird.ToList();
    birdList.Remove(bird);
    result.Tbl_Bird = birdList.ToArray();

    var updatedJsonStr = JsonConvert.SerializeObject(result, Formatting.Indented);
    File.WriteAllText(folderPath, updatedJsonStr);

    return Results.Ok(result.Tbl_Bird);
})
.WithName("DeleteBird")
.WithOpenApi();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}


public class BirdResponseModel
{
    public BirdModel[] Tbl_Bird { get; set; }
}

public class BirdModel
{
    public int Id { get; set; }
    public string BirdMyanmarName { get; set; }
    public string BirdEnglishName { get; set; }
    public string Description { get; set; }
    public string ImagePath { get; set; }
}
