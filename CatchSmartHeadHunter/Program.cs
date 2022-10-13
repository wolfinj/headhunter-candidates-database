using AutoMapper;
using CatchSmartHeadHunter.Core.Models;
using CatchSmartHeadHunter.Core.Services;
using CatchSmartHeadHunter.Data;
using CatchSmartHeadHunter.Helpers;
using CatchSmartHeadHunter.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<HhDbContext>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDbService, DbService>();
builder.Services.AddScoped<IEntityService<Company>, EntityService<Company>>();
builder.Services.AddScoped<IEntityService<Position>, EntityService<Position>>();
builder.Services.AddScoped<IEntityService<Candidate>, EntityService<Candidate>>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<ICandidateService, CandidateService>();
builder.Services.AddSingleton(RequestConverterExtensions.CreateMapper());
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
