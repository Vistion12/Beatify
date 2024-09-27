using Beatify.Database.Data;
using Beatify.Database.Repositories;
using Beatify.Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connection = builder.Configuration.GetConnectionString("BeatifyDataBaseContext");
// добавляем контекст ApplicationContext в качестве сервиса в приложение
builder.Services.AddDbContext<BeatifyDataBaseContext>(options =>
    options.UseSqlServer(connection));
builder.Services.AddControllers();
builder.Services.AddTransient<IGroupRepository, GroupRepository>();

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
