using Application.IRepository;
using Application.IService;
using Application.Serializer;
using Infrastructure.Context;
using Infrastructure.Repository;
using Infrastructure.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
      this IServiceCollection services,
      IConfiguration configuration) =>
      services
          .AddServices()
          .AddDatabase(configuration);
          //.AddIdentityOptions()
          //.AddBackgroundServices();

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IAppRepository<>), typeof(AppRepository<>));
           // services.AddScoped(typeof(IIdentityAppRepository<>), typeof(IdentityRepository<>));
            services.AddScoped<IJsonFieldsSerializer, JsonFieldsSerializer>();
            //services.AddScoped<DataSeeder>();
           
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<ITicketStatusService, TicketStatusService>();
            services.AddScoped<ITicketTraceService, TicketTraceService>();
            services.AddScoped<INotificationService, NotificationService>();
           // services.AddSignalR();

            services.AddSignalR();


            return services;
        }

        //private static IServiceCollection AddIdentityOptions(this IServiceCollection services)
        //{
        //    services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
        //    {
        //        options.SignIn.RequireConfirmedAccount = true;
        //        options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier;
        //        options.Lockout.AllowedForNewUsers = true;
        //        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
        //        options.Lockout.MaxFailedAccessAttempts = 5;
        //        options.Password.RequiredLength = 6;
        //        options.Password.RequireDigit = true;
        //        options.Password.RequireNonAlphanumeric = true;
        //    })
        //    .AddEntityFrameworkStores<IdentityAppDbContext>()
        //    .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);

        //    return services;
        //}

        private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<ApplicationDbContext>(options =>
            // options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("Default")));

            //services.AddDbContext<IdentityAppDbContext>(options =>
            //    options.UseSqlServer(configuration.GetConnectionString("IdentityConnection")));

            return services;
        }

        //private static IServiceCollection AddBackgroundServices(this IServiceCollection services)
        //{
        //    services.AddHostedService<BookingPaymentCheckService>();
        //    return services;
        //}














    }
}
