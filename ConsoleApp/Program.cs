using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using TextRepo.DataAccessLayer.Repositories;
using TextRepo.DataAccessLayer;
using TextRepo.Services;
using TextRepo.MainApp;
using NLog.Extensions.Logging;
using NLog;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(x =>
    {
        x.SetBasePath(Directory.GetCurrentDirectory());
        x.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    })
    .ConfigureServices((context, services) =>
    {
        services.AddOptions<DbSettingsModel>().Bind(context.Configuration.GetSection("Database"));
        services.AddScoped<DbContext, Context>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IDocumentRepository, DocumentRepository>();
        services.AddScoped<IContactRepository, ContactRepository>();
        services.AddTransient<UserService>();
        services.AddTransient<ProjectService>();
        services.AddTransient<DocumentService>();
        services.AddTransient<ContactService>();
        services.AddTransient<Logic>();
    })
    .ConfigureLogging((context, logBuilder) =>
    {
        LogManager.Configuration = new NLogLoggingConfiguration(context.Configuration.GetSection("NLog"));
        logBuilder.ClearProviders();
        logBuilder.AddNLog();
    })
    .Build();

runTest(host.Services);

host.Run();

static void runTest(IServiceProvider hostProvider)
{
    using IServiceScope serviceScope = hostProvider.CreateScope();
    IServiceProvider provider = serviceScope.ServiceProvider;
    Logic logic = provider.GetRequiredService<Logic>();

    logic.TestRun();

    LogManager.Shutdown();
}