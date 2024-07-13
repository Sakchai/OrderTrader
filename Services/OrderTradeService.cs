using Microsoft.Extensions.Configuration;
using OrderTrader.Models;
using Python.Runtime;
namespace OrderTrader.Services
{

    public class OrderTradeService : IOrderTradeService
    {
        private readonly IConfiguration _config;
        public OrderTradeService(IConfiguration config)
        {
            _config = config;
            var _scriptPath = $"{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)}\\Scripts\\";
            // Initialize the Python runtime
            Runtime.PythonDLL = _config.GetValue<string>("PythonDLL");
            PythonEngine.Initialize();

            // Add the script directory to the Python path
            using (Py.GIL())
            {
                dynamic sys = Py.Import("sys");
                var directory = System.IO.Path.GetDirectoryName(_scriptPath);
                sys.path.append(directory);
            }
        }

        public dynamic Add(int a, int b)
        {
            using (Py.GIL())
            {
                try
                {
                    // Import the Python script
                    dynamic settrade_v2 = Py.Import("settrade_v2");
                    Console.WriteLine("settrade_v2 version: " + settrade_v2.__version__);
                    dynamic urllib3 = Py.Import("urllib3");
                    Console.WriteLine("urllib3 version: " + urllib3.__version__);
                
                    dynamic pyScript = Py.Import("script");

                    // Call the add function
                    return pyScript.add(a, b);
                }
                catch (PythonException ex)
                {
                    Console.WriteLine($"Python exception: {ex.Message}");
                    throw;
                }
            }
        }

        public dynamic PlaceOrder(Transaction transaction)
        {
            using (Py.GIL())
            {
                try
                {
                    // Import the Python script using the script name without extension
                    dynamic pyScript = Py.Import("script");

                    // Create an instance of the Order class and call place_order
                    dynamic order = pyScript.Order(
                        transaction.investor.AppId,
                        transaction.investor.AppSecret,
                        transaction.investor.AppCode,
                        transaction.investor.BrokerId,
                        transaction.OrderType,
                        transaction.AccountNo,
                        transaction.Symbol,
                        transaction.Price,
                        transaction.Volume,
                        transaction.Side,
                        transaction.Pin,
                        transaction.Position,
                        transaction.PriceType,
                        transaction.ValidityType
                    );
                    return order.place_order();
                }
                catch (PythonException ex)
                {
                    Console.WriteLine($"Python exception: {ex.Message}");
                    throw;
                }
            }
        }
        ~OrderTradeService()
        {
            // Shutdown the Python runtime
            PythonEngine.Shutdown();
        }
    }

}
