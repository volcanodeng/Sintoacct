namespace Sintoacct.Ledger.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifySourceDocument1 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.T_Source_Document");
            AlterColumn("dbo.T_Source_Document", "FileId", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.T_Source_Document", "FileId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.T_Source_Document");
            AlterColumn("dbo.T_Source_Document", "FileId", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.T_Source_Document", "FileId");
        }
    }
}
