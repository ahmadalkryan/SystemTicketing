
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
      //      services.AddSignalR();

            return services;
        }
    }
}
