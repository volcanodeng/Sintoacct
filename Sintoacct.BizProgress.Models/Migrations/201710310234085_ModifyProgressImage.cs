namespace Sintoacct.Progress.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyProgressImage : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.T_Prog_ProgressImage", "AliyunBucket", c => c.String(maxLength: 64));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.T_Prog_ProgressImage", "AliyunBucket", c => c.String(nullable: false, maxLength: 64));
        }
    }
}
