namespace Sintoacct.Ledger.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyVoucher : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.T_Voucher", "VoucherYear");
            CreateIndex("dbo.T_Voucher", "VoucherMonth");
            CreateIndex("dbo.T_Voucher", "PaymentTerms");
        }
        
        public override void Down()
        {
            DropIndex("dbo.T_Voucher", new[] { "PaymentTerms" });
            DropIndex("dbo.T_Voucher", new[] { "VoucherMonth" });
            DropIndex("dbo.T_Voucher", new[] { "VoucherYear" });
        }
    }
}
