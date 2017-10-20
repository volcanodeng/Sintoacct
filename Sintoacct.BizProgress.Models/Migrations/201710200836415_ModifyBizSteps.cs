namespace Sintoacct.Progress.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyBizSteps : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.T_Prog_WorkOrderItem", "BizSteps_StepId", "dbo.T_Prog_BizSteps");
            DropIndex("dbo.T_Prog_WorkOrderItem", new[] { "BizSteps_StepId" });
            DropColumn("dbo.T_Prog_WorkOrderItem", "BizSteps_StepId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.T_Prog_WorkOrderItem", "BizSteps_StepId", c => c.Int());
            CreateIndex("dbo.T_Prog_WorkOrderItem", "BizSteps_StepId");
            AddForeignKey("dbo.T_Prog_WorkOrderItem", "BizSteps_StepId", "dbo.T_Prog_BizSteps", "StepId");
        }
    }
}
