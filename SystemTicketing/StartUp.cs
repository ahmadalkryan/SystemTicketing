using Application;
using Infrastructure;
using Infrastructure.Seeds;
using SystemTicketing.EXpectionMiddleWare;

namespace SystemTicketing
{
    public class StartUp(IConfiguration configuration)
    {
       
        
            public IConfiguration Configuration { get; } = configuration;

            public void ConfigureServices(IServiceCollection services)
            {
            services
                .AddApplication()
                .AddInfrastructure(Configuration);
                    //.AddPresentation(Configuration);
            }


            public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataSeeder dataSeeder)
            {
                if (env.IsDevelopment())
                {
                    app.UseExceptionHandler("/Home/Error");
                    app.UseHsts();
                }

                dataSeeder.SeedData();
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseHttpsRedirection();
                app.UseStaticFiles();
                app.UseRouting();
                 // app.MapHub<NotificationHub>("/notificationHub");
            app.UseCors("DevCors");

                app.UseAuthentication();
                app.UseAuthorization();

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My_API V1");
                });

            // app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseMiddleware<TicketNotificationMiddleware>();

            app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            }
        }

    
}
