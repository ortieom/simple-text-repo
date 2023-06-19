using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MainApp;
using Microsoft.EntityFrameworkCore;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services => {
        services.AddSingleton<DbContext, DataLib.Context>(
            x => new DataLib.Context("D:\\Projects\\dotnet-test\\ConsoleApp\\ConsoleApp\\storage.db"));
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
