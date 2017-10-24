namespace Sintoacct.Progress.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyCustomers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.T_Prog_Customers", "State", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.T_Prog_Customers", "State");
        }
    }
}
