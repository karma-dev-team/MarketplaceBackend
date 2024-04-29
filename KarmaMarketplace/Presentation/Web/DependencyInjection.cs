using Hangfire;
using Hangfire.PostgreSql;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Domain.User.Entities;
using KarmaMarketplace.Presentation.Web.ExceptionHandlers;
using KarmaMarketplace.Presentation.Web.Schemas.Filters;
using KarmaMarketplace.Presentation.Web.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json;

namespace KarmaMarketplace.Presentation.Web
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebServices(
            this IServiceCollection services,
            IConfiguration configuration, 
            IWebHostEnvironment environment, 
            ILoggingBuilder logging)
        {

            services.AddEndpointsApiExplorer();
            services.AddMvc().AddJsonOptions(c =>
                        c.JsonSerializerOptions.PropertyNamingPolicy
                            = JsonNamingPolicy.CamelCase);
            services.AddSwaggerGen(options =>
            {
                var info = new OpenApiInfo { Title = "KarmaMarketplace", Version = "v1" };
                options.SwaggerDoc(name: "v1", info: info);
                options.EnableAnnotations();
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid JWT token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                });
                options.SchemaFilter<StreamSchemaFilter>();

                options.UseInlineDefinitionsForEnums();

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
            services.AddExceptionHandler<GuardClauseExceptionHandler>();

            services.AddHangfire(config =>
                config
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UsePostgreSqlStorage(c =>
                        c.UseNpgsqlConnection(configuration["TasksStorage"])));

            services.AddHangfireServer();
            services.AddCoreAdmin();

            services.AddCors(option => option.AddPolicy("TaskManger", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            }));
            services.AddMemoryCache();

            services.AddHealthChecks();
            services.AddHttpClient();
            if (environment.IsProduction())
            {
                logging.AddSentry(options =>
                {
                    options.Dsn = configuration["SentryDsn"];
                    options.TracesSampleRate = 1.0;
                    options.Environment = "production";
                    options.Release = "app";
                });
            }
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                var secretKey = configuration["Jwt:SecretKey"];

                Guard.Against.Null(secretKey, message: "Jwt:SecretKey does not exists");

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });
            services.AddScoped<IPasswordHasher<UserEntity>, PasswordHasher<UserEntity>>();
            services.AddHttpContextAccessor();
            services.AddScoped<IUser, CurrentUser>();

            return services;    
        }
    }
}
