namespace Sintoacct.Ledger.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyVoucherDetail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.T_Voucher_Detail", "YtdDebitQuantity", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.T_Voucher_Detail", "YtdDebit", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.T_Voucher_Detail", "YtdCreditQuantity", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.T_Voucher_Detail", "YtdCredit", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.T_Voucher_Detail", "YtdCredit");
            DropColumn("dbo.T_Voucher_Detail", "YtdCreditQuantity");
            DropColumn("dbo.T_Voucher_Detail", "YtdDebit");
            DropColumn("dbo.T_Voucher_Detail", "YtdDebitQuantity");
        }
    }
}
