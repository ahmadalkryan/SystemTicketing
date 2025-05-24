using Application.Common;
using Application.IRepository;
using Application.IService;
using Application.Mapping.TicketProfile;
using Domain;
using Infrastructure.Common;
using Infrastructure.Context;
using Infrastructure.Repository;
using Infrastructure.Service;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//injection 
builder.Services.AddScoped(typeof(IAppRepository<>),typeof(AppRepository<>));
builder.Services.AddScoped<ITicketNumberGenerator, TicketNumberGenerator>();
builder.Services.AddScoped<TicketNumberResolver>();
builder.Services.AddScoped<ITicketService,TicketService>();
builder.Services.AddScoped<ITicketTraceService,TicketTraceService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<ITicketStausService,TicketStuatusService>();


// mapper
//builder.Services.AddAutoMapper(typeof(TicketProfile).Assembly);



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
