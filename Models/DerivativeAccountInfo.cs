namespace OrderTrader.Models
{
    public class DerivativeAccountInfo
    {
        public string CallForceFlag { get; set; }
        public double CallForceMargin { get; set; }
        public double CallForceMarginMM { get; set; }
        public double CashBalance { get; set; }
        public string ClosingMethod { get; set; }
        public double CreditLine { get; set; }
        public double DepositWithdrawal { get; set; }
        public double Equity { get; set; }
        public double ExcessEquity { get; set; }
        public double InitialMargin { get; set; }
        public double LiquidationValue { get; set; }
        public double TotalFM { get; set; }
        public double TotalMM { get; set; }
        public double TotalMR { get; set; }
    }

}
