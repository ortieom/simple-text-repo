using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using TextRepo.DataAccessLayer.Repositories;
using TextRepo.DataAccessLayer;
using TextRepo.Services;
using TextRepo.MainApp;
using NLog.Extensions.Logging;
using NLog;

var config = new ConfigurationBuilder()
    .AddJsonFile("D:\\Projects\\dotnet-test\\ConsoleApp\\ConsoleApp\\appsettings.json", optional: false,
        reloadOnChange: true)
    .Build();
LogManager.Configuration = new NLogLoggingConfiguration(config.GetSection("NLog"));

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddScoped<DbContext, TextRepo.DataAccessLayer.Context>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IDocumentRepository, DocumentRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddTransient<UserService>();
        services.AddTransient<ProjectService>();
        services.AddTransient<DocumentService>();
        services.AddTransient<Logic>();
    })
    .ConfigureLogging(x =>
    {
        x.ClearProviders();
        x.AddNLog();
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