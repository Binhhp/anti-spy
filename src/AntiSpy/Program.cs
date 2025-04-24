using AntiSpy.Infrastructure.Bases;
using AntiSpy.Infrastructure.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configurationRoot = new ConfigurationBuilder()
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .Build();
var appSettings = configurationRoot.GetSection("AppSetting").Get<AppSetting>();

if(appSettings != null)
{
    builder.Services.AddSingleton(appSettings);
}
builder.Services.InitCoreComponents();
builder.Services.UseChannels();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
