namespace Sintoacct.Progress.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyBizItems : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.T_Prog_BizItems", "ServicePrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.T_Prog_BizItems", "ServicePrice");
        }
    }
}
