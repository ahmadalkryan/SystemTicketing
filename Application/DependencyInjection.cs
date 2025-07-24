using Application.IService;
using Application.LDAP;
using Application.Mapping;
using Application.Mapping.Notifiction;
using Application.Mapping.RoleProfile;
using Application.Mapping.TicketProfile;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            
            services.AddAutoMapper(typeof(TicketProfile).Assembly);
            services.AddAutoMapper(typeof(TicketStarusProfile).Assembly);
            services.AddAutoMapper(typeof(TicketTraceProfile).Assembly);
            services.AddAutoMapper(typeof(DeviceCategoryProfile).Assembly);
            services.AddAutoMapper(typeof(NotificationProfile).Assembly);
            services.AddAutoMapper(typeof(RoleProfile).Assembly);
            services.AddAutoMapper(typeof(UserProfile).Assembly);
            services.AddAutoMapper(typeof(UserRoleProfile).Assembly);

           // services.AddScoped<TokenService>();
           // services.AddScoped<AuthenticationService>();
            



            //   services. AddSingleton<ITokenBlacklistService, TokenBlacklistService>();


            return services;

        }


    }
}
