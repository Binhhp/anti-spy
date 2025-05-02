using AntiSpy.Infrastructure.Bases;
using AntiSpy.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson();

var configurationRoot = new ConfigurationBuilder()
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .Build();
var appSettings = configurationRoot.GetSection("AppSetting").Get<AppSetting>();

if(appSettings != null)
{
    builder.Services.AddSingleton(appSettings);
}

builder.Services.AddCors(confg => confg.AddPolicy("AllowAll",
       p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

builder.Services.InitCoreComponents();
builder.Services.UseChannels();
builder.Services.RegisterSwaggerService();

builder.Services.AddDbContext<AntiSpyDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AntiSpy")));

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();
app.UseSwaggerUI(c =>
{
    c.HeadContent = "";
    c.InjectStylesheet("https://cdnjs.cloudflare.com/ajax/libs/swagger-ui/5.18.2/swagger-ui.css");
    c.InjectJavascript("https://cdnjs.cloudflare.com/ajax/libs/swagger-ui/5.18.2/swagger-ui-bundle.js");
    c.InjectJavascript("https://cdnjs.cloudflare.com/ajax/libs/swagger-ui/5.18.2/swagger-ui-standalone-preset.js");
});
app.UseCors(corBuilder => corBuilder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.Run();
