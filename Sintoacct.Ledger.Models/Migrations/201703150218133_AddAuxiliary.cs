namespace Sintoacct.Ledger.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAuxiliary : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.T_Auxiliary", "AuxCode", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.T_Auxiliary", "AuxName", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.T_Auxiliary", "Creator", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.T_Auxiliary", "Creator", c => c.String(maxLength: 50));
            AlterColumn("dbo.T_Auxiliary", "AuxName", c => c.String(maxLength: 20));
            AlterColumn("dbo.T_Auxiliary", "AuxCode", c => c.String(maxLength: 20));
        }
    }
}
