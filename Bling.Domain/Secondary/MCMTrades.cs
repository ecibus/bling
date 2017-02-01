using System;
using Bling.Domain.Extension;

namespace Bling.Domain.Secondary
{
    public class MCMTrades
    {
        public virtual string TradeNo { get; set; }
        public virtual string Program { get; set; }
        public virtual string Tran { get; set; }
        public virtual string Dealer { get; set; }
        public virtual DateTime TradeDate { get; set; }
        public virtual double Amount { get; set; }
        public virtual DateTime SettlementDate { get; set; }
        public virtual DateTime NotificationDate { get; set; }
        public virtual double Coupon { get; set; }
        public virtual double Price { get; set; }
        public virtual double Margin { get; set; }
        public virtual double LifeCap { get; set; }
        public virtual double Premium { get; set; }
        public virtual string Position { get; set; }

        public static string Header()
        {
            return "trade_no,program,tran,dealer,trade_date,amount,settlement_date,notification_date,coupon,price,margin,life_cap,premium";
        }

        public override string ToString()
        {
            return String.Format(
                "{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}," +
                "{10},{11},{12}"
                ,
                TradeNo, Program.R(), Tran.R(), Dealer.R(), TradeDate.ToShortDateString(), Amount, SettlementDate.ToShortDateString(),
                NotificationDate.ToShortDateString(), Coupon, Price, Margin, LifeCap, Premium
                );
        }
    }
}
