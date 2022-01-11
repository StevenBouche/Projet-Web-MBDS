using AssignmentAPI;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Environment);

Log.Logger = new LoggerConfiguration()
         .ReadFrom.Configuration(startup.Configuration)
         .WriteTo.Console(Serilog.Events.LogEventLevel.Debug, outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {Level:u3}] {Message:lj} <{ThreadId}><{ThreadName}> {NewLine}{Exception}")
         .MinimumLevel.Debug()
         .Enrich.FromLogContext()
         .Enrich.WithThreadId()
         .CreateLogger();

builder.Host.UseSerilog();

startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app, app.Environment);

try
{
    Log.Logger.Information("Application Starting");
    app.Run();
}
catch (Exception e)
{
    Log.Error(e.Message);
    Environment.Exit(1);
}

Environment.Exit(0);