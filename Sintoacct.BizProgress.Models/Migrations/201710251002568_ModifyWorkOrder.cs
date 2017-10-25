namespace Sintoacct.Progress.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyWorkOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.T_Prog_WorkOrder", "State", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.T_Prog_WorkOrder", "State");
        }
    }
}
