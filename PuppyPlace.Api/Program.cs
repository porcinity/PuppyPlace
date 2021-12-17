using MediatR;
using Microsoft.EntityFrameworkCore;
using PuppyPlace.Data;
using PuppyPlace.Repository;
using PuppyPlace.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PuppyPlaceContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PuppyPlaceContext")));

builder.Services.AddTransient<IDogsRepository, DogsRepository>();
builder.Services.AddTransient<IPersonsRepository, PersonsRepository>();
builder.Services.AddTransient<IAdoptionService, AdoptionService>();
builder.Services.AddMediatR(typeof(MediatorEntry).Assembly);
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();