namespace Sintoacct.Progress.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyCustomers : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.T_Prog_Customers", "PromId", "dbo.T_Prog_BizPromotion");
            DropIndex("dbo.T_Prog_Customers", new[] { "PromId" });
            AlterColumn("dbo.T_Prog_Customers", "PromId", c => c.Long());
            CreateIndex("dbo.T_Prog_Customers", "PromId");
            AddForeignKey("dbo.T_Prog_Customers", "PromId", "dbo.T_Prog_BizPromotion", "PromId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.T_Prog_Customers", "PromId", "dbo.T_Prog_BizPromotion");
            DropIndex("dbo.T_Prog_Customers", new[] { "PromId" });
            AlterColumn("dbo.T_Prog_Customers", "PromId", c => c.Long(nullable: false));
            CreateIndex("dbo.T_Prog_Customers", "PromId");
            AddForeignKey("dbo.T_Prog_Customers", "PromId", "dbo.T_Prog_BizPromotion", "PromId", cascadeDelete: true);
        }
    }
}
