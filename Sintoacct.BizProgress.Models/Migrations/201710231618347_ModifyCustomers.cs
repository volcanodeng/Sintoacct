namespace Sintoacct.Progress.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyCustomers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.T_Prog_Customers", "PromName", c => c.String(maxLength: 50));
            AlterColumn("dbo.T_Prog_Customers", "PromId", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.T_Prog_Customers", "PromId", c => c.String());
            DropColumn("dbo.T_Prog_Customers", "PromName");
        }
    }
}
