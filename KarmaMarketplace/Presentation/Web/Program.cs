using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using KarmaMarketplace.Application;
using KarmaMarketplace.Domain.User.Entities;
using KarmaMarketplace.Infrastructure;
using KarmaMarketplace.Infrastructure.Data;
using System.Text;
using System.Text.Json;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Presentation.Web.Services;
using KarmaMarketplace.Presentation.Web.Schemas.Filters;
using KarmaMarketplace.Presentation.Web.ExceptionHandlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMvc().AddJsonOptions(c =>
            c.JsonSerializerOptions.PropertyNamingPolicy
                = JsonNamingPolicy.CamelCase);
builder.Services.AddSwaggerGen(options =>
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

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddHealthChecks();
builder.Services.AddHttpClient();
if (builder.Environment.IsProduction()) {
    builder.Logging.AddSentry(options =>
    {
        options.Dsn = builder.Configuration["SentryDsn"];
        options.TracesSampleRate = 1.0;
        options.Environment = "production"; 
        options.Release = "app"; 
    });
}
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    var secretKey = builder.Configuration["Jwt:SecretKey"];

    Guard.Against.Null(secretKey, message: "Jwt:SecretKey does not exists"); 

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
});
builder.Services.AddScoped<IPasswordHasher<UserEntity>, PasswordHasher<UserEntity>>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUser, CurrentUser>();

builder.Services.AddApplicationServices();
builder.Services.AddExceptionHandler<GuardClauseExceptionHandler>();

builder.Services.AddCoreAdmin();

builder.Services.AddCors(option => option.AddPolicy("TaskManger", builder =>
{
    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
}));
builder.Services.AddMemoryCache();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "NameApp API");
        options.DefaultModelsExpandDepth(-1);
    });
    await app.InitialiseDatabaseAsync(); 
}
app.UseEventDispatcher(); 
app.UseStaticFiles();
app.MapDefaultControllerRoute();

app.UseHealthChecks("/health");
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();
app.UseWebSockets(new WebSocketOptions() { 
    KeepAliveInterval = TimeSpan.FromMinutes(2), 
});
app.UseCors("TaskManger");
app.MapControllers();

app.Run();