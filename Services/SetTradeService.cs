using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using OrderTrader.Models;
using Python.Runtime;
namespace OrderTrader.Services
{

    public class SetTradeService : ISetTradeService
    {
        private readonly IConfiguration _config;
        private readonly Investor _investor;
        public SetTradeService(IConfiguration config, 
            IOptions<Investor> investor)
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

            this._investor = investor.Value;
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
                        _investor.AppId,
                        _investor.AppSecret,
                        _investor.AppCode,
                        _investor.BrokerId,
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

        public async Task<DerivativeAccountInfo> GetDerivativeAccountInfoAsync()
        {
            var result = new DerivativeAccountInfo();
            using (Py.GIL())
            {
                try
                {
                    // Import the Python script using the script name without extension
                    dynamic pyScript = Py.Import("script");

                    // Create an instance of the Order class and call place_order
                    dynamic request = pyScript.DerivativeOrderDetailAccountInfo(
                        _investor.AppId,
                        _investor.AppSecret,
                        _investor.AppCode,
                        _investor.BrokerId,
                        _investor.DerivativeAccountNo
                    );
                    dynamic response = request.get_account_info();
                    result = await JsonSerializer.DeserializeAsync<DerivativeAccountInfo>(Convert.ToString(response));
                    return result;
                }
                catch (PythonException ex)
                {
                    Console.WriteLine($"Python exception: {ex.Message}");
                    return result;
                }
            }
        }

        public async Task<DerivativeOrderInfo> GetDerivativeOrderAsync(int orderNo)
        {
            var result = new DerivativeOrderInfo();
            using (Py.GIL())
            {
                try
                {
                    // Import the Python script using the script name without extension
                    dynamic pyScript = Py.Import("script");

                    // Create an instance of the Order class and call place_order
                    dynamic request = pyScript.DerivativeOrderDetail(
                        _investor.AppId,
                        _investor.AppSecret,
                        _investor.AppCode,
                        _investor.BrokerId,
                        _investor.DerivativeAccountNo,
                         orderNo
                    );
                    dynamic response = request.get_order();
                    result = await JsonSerializer.DeserializeAsync<DerivativeOrderInfo>(Convert.ToString(response));
                    return result;
                }
                catch (PythonException ex)
                {
                    Console.WriteLine($"Python exception: {ex.Message}");
                    return result;
                }
            }
        }

        ~SetTradeService()
        {
            // Shutdown the Python runtime
            PythonEngine.Shutdown();
        }
    }

}
