namespace Sintoacct.Ledger.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyVoucher : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.T_Voucher", "ReviewOpinion", c => c.String(maxLength: 1000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.T_Voucher", "ReviewOpinion");
        }
    }
}
