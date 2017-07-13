namespace Sintoacct.Ledger.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyVoucher : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.T_Voucher", "VoucherDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.T_Voucher", "VoucherDate");
        }
    }
}
