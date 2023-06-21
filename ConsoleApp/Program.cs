using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MainApp;
using Microsoft.EntityFrameworkCore;
using DataLib.Repositories;
using DataLib;
using Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;

var config = new ConfigurationBuilder()
    .AddJsonFile("D:\\Projects\\dotnet-test\\ConsoleApp\\ConsoleApp\\appsettings.json", optional: false, reloadOnChange: true)
    .Build();

LogManager.Configuration = new NLogLoggingConfiguration(config.GetSection("NLog"));

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services => {
        services.AddSingleton<DbContext, DataLib.Context>();
        services.AddSingleton<IUserRepository, UserRepository>();
        services.AddSingleton<IProjectRepository, ProjectRepository>();
        services.AddSingleton<IDocumentRepository, DocumentRepository>();
        services.AddSingleton<IUnitOfWork, UnitOfWork>();
        services.AddTransient<UserService>();
        services.AddTransient<ProjectService>();
        services.AddTransient<DocumentService>();
        services.AddTransient<Logic>();
    })
    .ConfigureLogging(x => {
        x.ClearProviders();
        x.AddNLog();
    })
    .Build();

runTest(host.Services);

host.Run();

static void runTest(IServiceProvider hostProvider) {
    using IServiceScope serviceScope = hostProvider.CreateScope();
    IServiceProvider provider = serviceScope.ServiceProvider;
    Logic logic = provider.GetRequiredService<Logic>();

    logic.TestRun();

    LogManager.Shutdown();
}
