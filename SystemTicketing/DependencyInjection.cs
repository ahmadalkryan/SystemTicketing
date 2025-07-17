
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SystemTicketing.EmailService;
using SystemTicketing.EmailService.EmailConfig;

namespace SystemTicketing
{
    public static  class DependencyInjection
    {
        public static IServiceCollection AddPresentation( this IServiceCollection services ,IConfiguration configuration)
        {
            services.AddScoped<TicketNotificationFilter>();

            services.Configure<EmailConfiguration>(
    configuration.GetSection("EmailConfiguration"));

            services.AddScoped<IEmailService, EmailSender>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidAudience = configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
        };
    });
            //      services.AddSignalR();

            return services;
        }
    }
}
