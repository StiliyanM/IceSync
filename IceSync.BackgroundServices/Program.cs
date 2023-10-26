using IceSync.BackgroundServices.Extensions;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        var configuration = hostContext.Configuration;


        services.ConfigureServices(configuration);

    })
    .Build();

host.Run();
