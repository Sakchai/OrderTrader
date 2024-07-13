namespace OrderTrader.Models
{
    public class DerivativeOrderInfo
    {
        public string AccountNo { get; set; }
        public int BalanceQty { get; set; }
        public bool CanCancel { get; set; }
        public bool CanChange { get; set; }
        public int? CancelId { get; set; }
        public int CancelQty { get; set; }
        public DateTime CancelTime { get; set; }
        public string Cpm { get; set; }
        public string EntryId { get; set; }
        public DateTime EntryTime { get; set; }
        public int IcebergVol { get; set; }
        public string IsStopOrderNotActivate { get; set; }
        public int MatchQty { get; set; }
        public int OrderNo { get; set; }
        public string Position { get; set; }
        public double Price { get; set; }
        public int PriceDigit { get; set; }
        public string PriceType { get; set; }
        public int Qty { get; set; }
        public int RejectCode { get; set; }
        public string RejectReason { get; set; }
        public string ShowStatus { get; set; }
        public string Side { get; set; }
        public string Status { get; set; }
        public string StatusMeaning { get; set; }
        public string Symbol { get; set; }
        public string TerminalType { get; set; }
        public string TfxOrderNo { get; set; }
        public string TrType { get; set; }
        public DateTime TradeDate { get; set; }
        public DateTime TransactionTime { get; set; }
        public string TriggerCondition { get; set; }
        public double TriggerPrice { get; set; }
        public string TriggerSession { get; set; }
        public string TriggerSymbol { get; set; }
        public DateTime? ValidToDate { get; set; }
        public string Validity { get; set; }
        public int Version { get; set; }
    }


}