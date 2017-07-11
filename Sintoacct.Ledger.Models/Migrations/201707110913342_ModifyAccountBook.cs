namespace Sintoacct.Ledger.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyAccountBook : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.T_Account", "AbId", "dbo.T_Account_Book");
            DropForeignKey("dbo.T_Voucher", "AbId", "dbo.T_Account_Book");
            DropForeignKey("dbo.T_Certificate_Word", "AbId", "dbo.T_Account_Book");
            DropForeignKey("dbo.T_Auxiliary", "AbId", "dbo.T_Account_Book");
            DropForeignKey("dbo.T_Auxiliary_Type", "AbId", "dbo.T_Account_Book");
            DropForeignKey("dbo.T_User_Book", "AbId", "dbo.T_Account_Book");
            DropForeignKey("dbo.T_Voucher_Detail_Template", "AbId", "dbo.T_Account_Book");
            DropForeignKey("dbo.T_Abstract_Temp", "AbId", "dbo.T_Account_Book");
            DropPrimaryKey("dbo.T_Account_Book");
            AlterColumn("dbo.T_Account_Book", "AbId", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.T_Account_Book", "AbId");
            AddForeignKey("dbo.T_Account", "AbId", "dbo.T_Account_Book", "AbId", cascadeDelete: true);
            AddForeignKey("dbo.T_Voucher", "AbId", "dbo.T_Account_Book", "AbId", cascadeDelete: true);
            AddForeignKey("dbo.T_Certificate_Word", "AbId", "dbo.T_Account_Book", "AbId", cascadeDelete: true);
            AddForeignKey("dbo.T_Auxiliary", "AbId", "dbo.T_Account_Book", "AbId", cascadeDelete: true);
            AddForeignKey("dbo.T_Auxiliary_Type", "AbId", "dbo.T_Account_Book", "AbId");
            AddForeignKey("dbo.T_User_Book", "AbId", "dbo.T_Account_Book", "AbId", cascadeDelete: true);
            AddForeignKey("dbo.T_Voucher_Detail_Template", "AbId", "dbo.T_Account_Book", "AbId", cascadeDelete: true);
            AddForeignKey("dbo.T_Abstract_Temp", "AbId", "dbo.T_Account_Book", "AbId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.T_Abstract_Temp", "AbId", "dbo.T_Account_Book");
            DropForeignKey("dbo.T_Voucher_Detail_Template", "AbId", "dbo.T_Account_Book");
            DropForeignKey("dbo.T_User_Book", "AbId", "dbo.T_Account_Book");
            DropForeignKey("dbo.T_Auxiliary_Type", "AbId", "dbo.T_Account_Book");
            DropForeignKey("dbo.T_Auxiliary", "AbId", "dbo.T_Account_Book");
            DropForeignKey("dbo.T_Certificate_Word", "AbId", "dbo.T_Account_Book");
            DropForeignKey("dbo.T_Voucher", "AbId", "dbo.T_Account_Book");
            DropForeignKey("dbo.T_Account", "AbId", "dbo.T_Account_Book");
            DropPrimaryKey("dbo.T_Account_Book");
            AlterColumn("dbo.T_Account_Book", "AbId", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.T_Account_Book", "AbId");
            AddForeignKey("dbo.T_Abstract_Temp", "AbId", "dbo.T_Account_Book", "AbId", cascadeDelete: true);
            AddForeignKey("dbo.T_Voucher_Detail_Template", "AbId", "dbo.T_Account_Book", "AbId", cascadeDelete: true);
            AddForeignKey("dbo.T_User_Book", "AbId", "dbo.T_Account_Book", "AbId", cascadeDelete: true);
            AddForeignKey("dbo.T_Auxiliary_Type", "AbId", "dbo.T_Account_Book", "AbId");
            AddForeignKey("dbo.T_Auxiliary", "AbId", "dbo.T_Account_Book", "AbId", cascadeDelete: true);
            AddForeignKey("dbo.T_Certificate_Word", "AbId", "dbo.T_Account_Book", "AbId", cascadeDelete: true);
            AddForeignKey("dbo.T_Voucher", "AbId", "dbo.T_Account_Book", "AbId", cascadeDelete: true);
            AddForeignKey("dbo.T_Account", "AbId", "dbo.T_Account_Book", "AbId", cascadeDelete: true);
        }
    }
}
