using Microsoft.EntityFrameworkCore;
using MinAPI.Data;
using MinAPI.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//pridani databaze do asp.net core
builder.Services.AddDbContext<StatsDb>(opt => opt.UseSqlite("Data Source=stats.db"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/hello", () => "hello");

// POST -> /stats
// GET -> /stats/5
// GET -> /sats/all

app.MapPost("/stats", (StatsDb db, StatsResult result) =>
{
    db.StatsResults.Add(result);
    db.SaveChanges();

    return Results.Created($"/stats/{result.Id}", result);
});

app.MapGet("/stats/{id}", (StatsDb db, int id) =>
{
    var result = db.StatsResults.Where(x => x.Id == id).FirstOrDefault();
    if(result != null)
    {
        return Results.Ok(result);
    }
    else
    {
        return Results.NotFound();
    }
});


app.MapGet("/stats/{id}", (int id) => 
{
    StatsResult sr = new();
    sr.Id = id;
    sr.Source = "dummy result";
    return sr;
});

app.MapGet("/stats/all", (StatsDb db) => GetAllResults(db));


app.Run();

static List<StatsResult> GetAllResults(StatsDb db)
{
    return db.StatsResults.ToList();
}