namespace OrderTrader.Models
{
    public class Transaction
    {
        public Transaction()
        {
            investor = new Investor();
        }
        public string OrderType { get; set; }
        public string AccountNo { get; set; }
        public string Symbol { get; set; }
        public double Price { get; set; }
        public int Volume { get; set; }
        public string Side { get; set; }
        public string Pin { get; set; }
        public string Position { get; set; }
        public string PriceType { get; set; }
        public string ValidityType { get; set; }
        public Investor investor { get; set; }
    }

    public class Investor
    {
        public string AppId { get; set; }
        public string AppSecret { get; set; }
        public string AppCode { get; set; }
        public string BrokerId { get; set; }
    }

}