using KarmaMarketplace.Application;
using KarmaMarketplace.Infrastructure;
using KarmaMarketplace.Infrastructure.Data;
using KarmaMarketplace.Domain;
using Hangfire;
using KarmaMarketplace.Infrastructure.EventSourcing;
using KarmaMarketplace.Presentation.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebServices(builder.Configuration, builder.Environment, builder.Logging); 
builder.Services.AddDomainServices(); 
builder.Services.AddApplicationServices();

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
    await app.InitialiseEventStoreDatabaseAsync(); 
}
app.UseEventDispatcher(); 
app.UseStaticFiles();
app.MapDefaultControllerRoute();

app.UseHealthChecks("/health");
app.UseHttpsRedirection();

app.UseHangfireDashboard();
app.UseAuthorization();
app.UseAuthentication();
app.UseWebSockets(new WebSocketOptions() { 
    KeepAliveInterval = TimeSpan.FromMinutes(2), 
});
app.UseCors("TaskManger");
app.MapControllers();

app.Run();

// for testing! 
public partial class Program { }