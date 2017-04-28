namespace Sintoacct.Ledger.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifySourceDocument : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.T_Source_Document", "FileSize", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.T_Source_Document", "FileSize", c => c.Int(nullable: false));
        }
    }
}
