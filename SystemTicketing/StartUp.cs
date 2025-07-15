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
                .AddInfrastructure(Configuration)
                    .AddPresentation(Configuration);
        }

        private void AddPresentation(IConfiguration configuration)
        {
            throw new NotImplementedException();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataSeeder dataSeeder)
        {
            // Seed data first before middleware setup
            dataSeeder.SeedData();

            // Environment-specific configuration
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();  // Correct for development
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();  // HSTS should only be used in production
            }

            // Global exception handling middleware - placed immediately after env config
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            // Security and static content
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            // Authentication and Authorization - must come after UseRouting()
            app.UseAuthentication();
            app.UseAuthorization();  // Only call once!

            // CORS - should come after auth but before endpoints
            app.UseCors("DevCors");

            // SignalR hub mapping
           // app.MapHub<NotificationHub>("/notificationHub");

            // Custom middleware
            app.UseMiddleware<TicketNotificationMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<NotificationHub>("/notificationHub"); // Fixed position
                endpoints.MapControllers();
            });
            // Swagger configuration
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My_API V1");
            });

            // Endpoint configuration
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});
        }
    }
    
}








//public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataSeeder dataSeeder)
//    {
//        if (env.IsDevelopment())
//        {
//            app.UseExceptionHandler("/Home/Error");
//            app.UseHsts();
//        }

//        dataSeeder.SeedData();
//    app.UseMiddleware<ExceptionHandlingMiddleware>();
//    app.UseHttpsRedirection();

//        app.UseStaticFiles();
//        app.UseRouting();



//    app.UseCors("DevCors");
//    app.MapHub<NotificationHub>("/notificationHub");
//    app.UseAuthentication();
//        app.UseAuthorization();

//        app.UseSwagger();
//        app.UseSwaggerUI(c =>
//        {
//            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My_API V1");
//        });

//     app.UseMiddleware<ExceptionHandlingMiddleware>();
//      app.UseMiddleware<TicketNotificationMiddleware>();

//    app.UseEndpoints(endpoints =>
//        {
//            endpoints.MapControllers();
//        });
//    }
//}

