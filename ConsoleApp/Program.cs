using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MainApp;
using Microsoft.EntityFrameworkCore;
using DataLib.Repositories;
using DataLib;
using Services;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services => {
        services.AddSingleton<DbContext, DataLib.Context>(
            x => new DataLib.Context("D:\\Projects\\dotnet-test\\ConsoleApp\\ConsoleApp\\storage.db"));
        services.AddSingleton<IUserRepository, UserRepository>();
        services.AddSingleton<IProjectRepository, ProjectRepository>();
        services.AddSingleton<IDocumentRepository, DocumentRepository>();
        services.AddSingleton<IUnitOfWork, UnitOfWork>();
        services.AddTransient<UserService>();
        services.AddTransient<ProjectService>();
        services.AddTransient<DocumentService>();
        services.AddTransient<Logic>();
    })
    .Build();

runTest(host.Services);

host.Run();

static void runTest(IServiceProvider hostProvider) {
    using IServiceScope serviceScope = hostProvider.CreateScope();
    IServiceProvider provider = serviceScope.ServiceProvider;
    Logic logic = provider.GetRequiredService<Logic>();

    logic.TestRun();
}
