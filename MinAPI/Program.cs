using WPFTextGUI.Model;

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

app.MapGet("/hello", () => "hello");

// POST -> /stats
// GET -> /stats/5
// GET -> /sats/all

app.MapPost("/stats", (StatsResult result) =>
{
    return "Ok";
});


app.MapGet("/stats/{id}", (int id) => 
{
    StatsResult sr = new();
    sr.Id = id;
    sr.Source = "dummy result";
    return sr;
});

app.MapGet("/stats/all", GetAllResults);


app.Run();

static List<StatsResult> GetAllResults()
{
    return new List<StatsResult>()
    {
        new StatsResult { Id = 1, Source = "dummy result"},
        new StatsResult { Id = 2, Source = "dummy result"},
        new StatsResult { Id = 3, Source = "dummy result"}
    };
}