using BinanceInfoTelegramBot;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<TelegramBotService>();
    })
    .Build();

await host.RunAsync();
