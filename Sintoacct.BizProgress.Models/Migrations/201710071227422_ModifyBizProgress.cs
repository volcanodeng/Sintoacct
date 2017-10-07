namespace Sintoacct.Progress.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyBizProgress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.T_Prog_BizProgress", "Creator", c => c.String(maxLength: 50));
            AddColumn("dbo.T_Prog_BizProgress", "CreateTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.T_Prog_BizProgress", "CreateTime");
            DropColumn("dbo.T_Prog_BizProgress", "Creator");
        }
    }
}
