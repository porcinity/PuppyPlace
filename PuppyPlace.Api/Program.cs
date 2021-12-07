using Microsoft.EntityFrameworkCore;
using PuppyPlace.Data;
using Newtonsoft.Json;
using PuppyPlace.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PuppyPlaceContext>(options =>

    options.UseNpgsql(builder.Configuration.GetConnectionString("PuppyPlaceContext")));

builder.Services.AddTransient<IDogsService, DogsService>();
builder.Services.AddTransient<IPersonsService, PersonsService>();
builder.Services.AddTransient<IAdoptionService, AdoptionService>();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});

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

app.UseAuthorization();

app.MapControllers();

app.Run();