using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using OrderTrader.Models;
using OrderTrader.Services;

partial class Program
{
    static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        var pythonService = host.Services.GetRequiredService<IOrderTradeService>();

        // Use the Python service
        var result = pythonService.Add(10, 20);
        Console.WriteLine($"Result from Python script: {result}");
        var investor = host.Services.GetRequiredService<Investor>();
        // Create a transaction for Derivatives order
        var derivativesTransaction = new Transaction
        {
            OrderType = "Derivatives",
            AccountNo = "settrade-D",
            Symbol = "S50M22",
            Price = 950.0,
            Volume = 13,
            Side = "SHORT",
            Pin = "000000",
            Position = "AUTO",
            PriceType = "LIMIT",
            ValidityType = "GOOD_TILL_DAY",
            investor = investor
        };

        // Use the Python service to place a Derivatives order
        var derivativesOrderResult = pythonService.PlaceOrder(derivativesTransaction);
        Console.WriteLine($"Derivatives order result from Python script: {derivativesOrderResult}");

        // Create a transaction for Equity order
        var equityTransaction = new Transaction
        {
            OrderType = "Equity",
            AccountNo = "settrade-E",
            Symbol = "PTT",
            Price = 38.0,
            Volume = 100,
            Side = "BUY",
            Pin = "000000",
            investor = investor
        };

        // Use the Python service to place an Equity order
        var equityOrderResult = pythonService.PlaceOrder(equityTransaction);
        Console.WriteLine($"Equity order result from Python script: {equityOrderResult}");


    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((context, config) =>
        {
            config.AddJsonFile("appSettings.json", optional: false, reloadOnChange: true);
        })
        .ConfigureServices((context, services) =>
        {
            // Bind configuration section to Investor class
            services.Configure<Investor>(context.Configuration.GetSection("Investor"));

            // Register Investor as a singleton
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<Investor>>().Value);
            services.AddSingleton<IOrderTradeService, OrderTradeService>();
        });
}
