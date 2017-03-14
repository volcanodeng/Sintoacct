namespace Sintoacct.Ledger.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAuxiliaryType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.T_Auxiliary_Type", "AuxType", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.T_Auxiliary_Type", "AuxType", c => c.String(maxLength: 20));
        }
    }
}
