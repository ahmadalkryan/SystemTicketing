using Application.IRepository;
using Application.IService;
using Infrastructure.Context;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using SystemTicketing;






public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
        
    }


    public static IHostBuilder CreateHostBuilder(string[] args) =>

        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}

























//var builder = WebApplication.CreateBuilder(args);

//// ����� AutoMapper ��� �����
//builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

//// ������� ����� ��������
//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

//// ������� ������
//builder.Services.AddControllers();

//// ������� Swagger
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My_API", Version = "v1" });
//});

//// ��� �������
//builder.Services.AddScoped(typeof(IAppRepository<>), typeof(AppRepository<>));
//// ... ���� �������

//// CORS
//builder.Services.AddCors(option => option.AddPolicy("AllowAll",
//    builder => builder.AllowAnyOrigin()
//                      .AllowAnyMethod()
//                      .AllowAnyHeader()));

//var app = builder.Build();

//// ����� Middleware
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My_API V1"));
//}

//app.UseHttpsRedirection();
//app.UseRouting();
//app.UseCors("AllowAll");
//app.UseAuthorization();
//app.MapControllers();
//app.Run();