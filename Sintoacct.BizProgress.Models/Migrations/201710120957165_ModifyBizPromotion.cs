namespace Sintoacct.Progress.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyBizPromotion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.T_Prog_BizPromotion", "WeixinOpenId", c => c.String());
            AlterColumn("dbo.T_Prog_BizPromotion", "ParentPromId", c => c.Long());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.T_Prog_BizPromotion", "ParentPromId", c => c.Long(nullable: false));
            DropColumn("dbo.T_Prog_BizPromotion", "WeixinOpenId");
        }
    }
}
