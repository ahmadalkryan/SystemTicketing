using Application.IRepository;
using Infrastructure.Context;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// ≈÷«›… AutoMapper „—… Ê«Õœ…
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// ≈⁄œ«œ«  ﬁ«⁄œ… «·»Ì«‰« 
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// ≈⁄œ«œ«  «· Õﬂ„
builder.Services.AddControllers();

// ≈⁄œ«œ«  Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My_API", Version = "v1" });
});

// Õﬁ‰ «·Œœ„« 
builder.Services.AddScoped(typeof(IAppRepository<>), typeof(AppRepository<>));
// ... »«ﬁÌ «·Œœ„« 

// CORS
builder.Services.AddCors(option => option.AddPolicy("AllowAll",
    builder => builder.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader()));

var app = builder.Build();

// ŒÿÊ«  Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My_API V1"));
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();
app.Run();