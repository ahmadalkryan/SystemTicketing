
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using SystemTicketing.EXpectionMiddleWare;

namespace SystemTicketing
{
    public static  class DependencyInjection
    {
        public static IServiceCollection AddPresentation( this IServiceCollection services ,IConfiguration configuration)
        {
            services.AddScoped<TicketNotificationFilter>();


           //// أو كـ Scoped إذا كان يعتمد على خدمات أخرى
           //services.AddScoped<ExceptionHandlingMiddleware>();

            services.AddEndpointsApiExplorer();

            services.AddControllers();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme= JwtBearerDefaults.AuthenticationScheme;
            })
    .AddJwtBearer(options =>
    {
       
        options.SaveToken=true;
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
            services.AddAuthorization();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "My_API",
                Version = "v1",
                Contact = new OpenApiContact()
                {
                    Name=" Ahmad Amen"

                }
            });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Bearer Authentication with JWT Token.'"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Name="Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()  
                   // Array.Empty<string>()
                    }
                });
        });



        services.AddCors(options => {
            options.AddPolicy("DevCors", policy => {
                policy.WithOrigins(
                        "https://localhost:52324", // React port
                        "https://localhost:7220"   // API port
                    )
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();
            });
        });



            return services;
        }
    }
}
//options.Events = new JwtBearerEvents
//{
//    OnTokenValidated = async context =>
//    {
//        var token = context.SecurityToken?.RawData;
//        var tokenBlacklistService = context.HttpContext.RequestServices
//            .GetRequiredService<ITokenBlacklistService>();

//        if (!string.IsNullOrEmpty(token) &&
//            tokenBlacklistService.IsTokenBlacklisted(token))
//        {
//            context.Fail("Token is blacklisted");
//        }
//    }
//};

//      services.AddSignalR();