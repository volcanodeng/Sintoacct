namespace Sintoacct.Progress.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyCustomers : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.T_Prog_Customers", "CustomerName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.T_Prog_Customers", "CustomerAddress", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.T_Prog_Customers", "Contacts", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.T_Prog_Customers", "Level", c => c.Int(nullable: false));
            AlterColumn("dbo.T_Prog_Customers", "PromName", c => c.String(nullable: false, maxLength: 150));
            DropColumn("dbo.T_Prog_Customers", "PromId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.T_Prog_Customers", "PromId", c => c.String(maxLength: 50));
            AlterColumn("dbo.T_Prog_Customers", "PromName", c => c.String(maxLength: 50));
            AlterColumn("dbo.T_Prog_Customers", "Level", c => c.Int());
            AlterColumn("dbo.T_Prog_Customers", "Contacts", c => c.String(maxLength: 50));
            AlterColumn("dbo.T_Prog_Customers", "CustomerAddress", c => c.String(maxLength: 200));
            AlterColumn("dbo.T_Prog_Customers", "CustomerName", c => c.String(maxLength: 50));
        }
    }
}
