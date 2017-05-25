namespace Sintoacct.Ledger.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyVoucher : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.T_Voucher", "VoucherYear", c => c.Int(nullable: false));
            AddColumn("dbo.T_Voucher", "VoucherMonth", c => c.Int(nullable: false));
            CreateIndex("dbo.T_Voucher", "VoucherDate");
        }
        
        public override void Down()
        {
            DropIndex("dbo.T_Voucher", new[] { "VoucherDate" });
            DropColumn("dbo.T_Voucher", "VoucherMonth");
            DropColumn("dbo.T_Voucher", "VoucherYear");
        }
    }
}
