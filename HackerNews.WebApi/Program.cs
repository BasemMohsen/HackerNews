using HackerNews.ApiClient.Configuration;
using HackerNews.ApiClient.Implementation;
using HackerNews.ApiClient.Interfaces;
using HackerNews.Services.Configurations;
using HackerNews.Services.Interfaces;
using HackerNews.Services.Profiles;
using HackerNews.Services.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddScoped<IHackerNewsApiClient, HackerNewsApiClient>();
builder.Services.AddScoped<IStoryService, StoryService>();
builder.Services.AddMemoryCache();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.Configure<CacheConfig>(builder.Configuration.GetSection("CacheConfig"));
builder.Services.Configure<HackerNewsApiConfig>(builder.Configuration.GetSection("HackerNewsApiConfig"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
