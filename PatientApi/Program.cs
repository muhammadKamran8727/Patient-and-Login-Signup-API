
using Microsoft.EntityFrameworkCore;

using PatientApi.IBusinessLogic;
using PatientApi.Models;
using Microsoft.Extensions.Configuration;

using PatientApi.BusinessLogic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var provider = builder.Services.BuildServiceProvider();

builder.Services.AddScoped<ILoginBL,LoginBL>();
builder.Services.AddScoped<IPatientBL, PatientBL>();
builder.Services.AddScoped<IScheduleBL, ScheduleBL>();





var Configuration = provider.GetService<IConfiguration>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PBase>(options => options.UseSqlServer(Configuration.GetConnectionString("sql-conn")));

var app = builder.Build();

app.UseCors(builder => builder
       .AllowAnyHeader()
       .AllowAnyMethod()
       .AllowAnyOrigin()
    );
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
