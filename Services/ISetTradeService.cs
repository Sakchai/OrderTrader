using OrderTrader.Models;

namespace OrderTrader.Services
{
    public interface ISetTradeService
    {
        dynamic Add(int a, int b);
        dynamic PlaceOrder(Transaction transaction);
        Task<DerivativeAccountInfo> GetDerivativeAccountInfoAsync();
        Task<DerivativeOrderInfo> GetDerivativeOrderAsync(int orderNo);
    }

}
