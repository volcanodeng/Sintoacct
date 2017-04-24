namespace Sintoacct.Ledger.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSourceDocument : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.T_Source_Document",
                c => new
                    {
                        FileId = c.Guid(nullable: false),
                        SourceFileName = c.String(nullable: false, maxLength: 255),
                        FullFileName = c.String(nullable: false, maxLength: 255),
                        RelateFileName = c.String(nullable: false, maxLength: 255),
                        RelatePath = c.String(nullable: false, maxLength: 255),
                        FileSize = c.Int(nullable: false),
                        VId = c.Long(),
                    })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("dbo.T_Voucher", t => t.VId)
                .Index(t => t.VId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.T_Source_Document", "VId", "dbo.T_Voucher");
            DropIndex("dbo.T_Source_Document", new[] { "VId" });
            DropTable("dbo.T_Source_Document");
        }
    }
}
