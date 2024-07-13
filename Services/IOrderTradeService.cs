using OrderTrader.Models;

namespace OrderTrader.Services
{
    public interface IOrderTradeService
    {
        dynamic Add(int a, int b);
        dynamic PlaceOrder(Transaction transaction);
    }

}
