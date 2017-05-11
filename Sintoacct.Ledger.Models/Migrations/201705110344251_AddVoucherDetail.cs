namespace Sintoacct.Ledger.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVoucherDetail : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.T_Voucher_Detail", new[] { "Account_AccId" });
            RenameColumn(table: "dbo.T_Voucher_Detail", name: "Account_AccId", newName: "AccId");
            AlterColumn("dbo.T_Voucher_Detail", "AccId", c => c.Long(nullable: false));
            CreateIndex("dbo.T_Voucher_Detail", "AccId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.T_Voucher_Detail", new[] { "AccId" });
            AlterColumn("dbo.T_Voucher_Detail", "AccId", c => c.Long());
            RenameColumn(table: "dbo.T_Voucher_Detail", name: "AccId", newName: "Account_AccId");
            CreateIndex("dbo.T_Voucher_Detail", "Account_AccId");
        }
    }
}
