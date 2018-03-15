namespace Sintoacct.Progress.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyWorkProgress1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.T_Prog_WorkProgress", "IsSuccess", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.T_Prog_WorkProgress", "IsSuccess");
        }
    }
}
