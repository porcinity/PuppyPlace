using Microsoft.EntityFrameworkCore;
using PuppyPlace.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PuppyPlaceContext>(options =>

    options.UseNpgsql(builder.Configuration.GetConnectionString("PuppyPlaceContext")));

// ContainerConfig.Configure();

// Add services to the container.

// builder.Services.AddControllers();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
 {
     options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
 });
builder.Services.AddDbContext<PuppyPlaceContext>(opt =>
    opt.UseNpgsql(connectionString: @"Host=localhost;Username=test;Password=test;Database=PuppyPlace"));

builder.Services.AddTransient<IDogsService, DogsService>();
builder.Services.AddTransient<IPersonsService, PersonsService>();

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

app.UseAuthorization();

app.MapControllers();

app.Run();