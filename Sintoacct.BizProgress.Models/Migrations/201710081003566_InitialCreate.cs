namespace Sintoacct.Progress.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.T_Prog_BizCategory",
                c => new
                    {
                        CateId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(maxLength: 50),
                        SortIndex = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CateId);
            
            CreateTable(
                "dbo.T_Prog_BizItems",
                c => new
                    {
                        ItemId = c.Int(nullable: false, identity: true),
                        ItemName = c.String(maxLength: 50),
                        SortIndex = c.Int(nullable: false),
                        CateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItemId)
                .ForeignKey("dbo.T_Prog_BizCategory", t => t.CateId, cascadeDelete: true)
                .Index(t => t.CateId);
            
            CreateTable(
                "dbo.T_Prog_BizProgress",
                c => new
                    {
                        BizId = c.Long(nullable: false, identity: true),
                        CusId = c.Long(nullable: false),
                        ContractTime = c.DateTime(nullable: false),
                        CateId = c.Int(nullable: false),
                        ItemId = c.Int(nullable: false),
                        StepId = c.Int(nullable: false),
                        ProgressDesc = c.String(maxLength: 500),
                        Remark = c.String(),
                        BizManager = c.String(maxLength: 50),
                        BizOperations = c.String(maxLength: 100),
                        Creator = c.String(maxLength: 50),
                        CreateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.BizId)
                .ForeignKey("dbo.T_Prog_BizCategory", t => t.CateId)
                .ForeignKey("dbo.T_Prog_BizItems", t => t.ItemId)
                .ForeignKey("dbo.T_Prog_BizSteps", t => t.StepId)
                .ForeignKey("dbo.T_Prog_Customers", t => t.CusId)
                .Index(t => t.CusId)
                .Index(t => t.CateId)
                .Index(t => t.ItemId)
                .Index(t => t.StepId);
            
            CreateTable(
                "dbo.T_Prog_BizSteps",
                c => new
                    {
                        StepId = c.Int(nullable: false, identity: true),
                        StepName = c.String(maxLength: 50),
                        SortIndex = c.Int(nullable: false),
                        CateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StepId)
                .ForeignKey("dbo.T_Prog_BizCategory", t => t.CateId, cascadeDelete: true)
                .Index(t => t.CateId);
            
            CreateTable(
                "dbo.T_Prog_Customers",
                c => new
                    {
                        CusId = c.Long(nullable: false, identity: true),
                        CustomerName = c.String(maxLength: 50),
                        CustomerAddress = c.String(maxLength: 200),
                        BusinessAddress = c.String(maxLength: 200),
                        Contacts = c.String(maxLength: 50),
                        Phone = c.String(maxLength: 20),
                        Email = c.String(maxLength: 50),
                        WeixinNick = c.String(maxLength: 50),
                        Level = c.Int(nullable: false),
                        PromId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.CusId)
                .ForeignKey("dbo.T_Prog_BizPromotion", t => t.PromId, cascadeDelete: true)
                .Index(t => t.PromId);
            
            CreateTable(
                "dbo.T_Prog_BizPromotion",
                c => new
                    {
                        PromId = c.Long(nullable: false, identity: true),
                        ParentPromId = c.Long(nullable: false),
                        OpName = c.String(maxLength: 50),
                        PromLevel = c.Int(nullable: false),
                        PromChain = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.PromId);
            
            CreateTable(
                "dbo.T_Prog_ProgressImage",
                c => new
                    {
                        ImgId = c.Long(nullable: false, identity: true),
                        OriginalImageName = c.String(),
                        ServerImageName = c.String(),
                        BizId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ImgId)
                .ForeignKey("dbo.T_Prog_BizProgress", t => t.BizId, cascadeDelete: true)
                .Index(t => t.BizId);
            
            CreateTable(
                "dbo.T_Prog_CustomerCost",
                c => new
                    {
                        CusId = c.Long(nullable: false),
                        ItemId = c.Int(nullable: false),
                        AmountReceivable = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PreferentialAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AmountReceived = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CollectionDays = c.String(maxLength: 100),
                        CreditingType = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => new { t.CusId, t.ItemId })
                .ForeignKey("dbo.T_Prog_BizItems", t => t.ItemId, cascadeDelete: true)
                .ForeignKey("dbo.T_Prog_Customers", t => t.CusId, cascadeDelete: true)
                .Index(t => t.CusId)
                .Index(t => t.ItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.T_Prog_CustomerCost", "CusId", "dbo.T_Prog_Customers");
            DropForeignKey("dbo.T_Prog_CustomerCost", "ItemId", "dbo.T_Prog_BizItems");
            DropForeignKey("dbo.T_Prog_ProgressImage", "BizId", "dbo.T_Prog_BizProgress");
            DropForeignKey("dbo.T_Prog_BizProgress", "CusId", "dbo.T_Prog_Customers");
            DropForeignKey("dbo.T_Prog_Customers", "PromId", "dbo.T_Prog_BizPromotion");
            DropForeignKey("dbo.T_Prog_BizProgress", "StepId", "dbo.T_Prog_BizSteps");
            DropForeignKey("dbo.T_Prog_BizSteps", "CateId", "dbo.T_Prog_BizCategory");
            DropForeignKey("dbo.T_Prog_BizProgress", "ItemId", "dbo.T_Prog_BizItems");
            DropForeignKey("dbo.T_Prog_BizProgress", "CateId", "dbo.T_Prog_BizCategory");
            DropForeignKey("dbo.T_Prog_BizItems", "CateId", "dbo.T_Prog_BizCategory");
            DropIndex("dbo.T_Prog_CustomerCost", new[] { "ItemId" });
            DropIndex("dbo.T_Prog_CustomerCost", new[] { "CusId" });
            DropIndex("dbo.T_Prog_ProgressImage", new[] { "BizId" });
            DropIndex("dbo.T_Prog_Customers", new[] { "PromId" });
            DropIndex("dbo.T_Prog_BizSteps", new[] { "CateId" });
            DropIndex("dbo.T_Prog_BizProgress", new[] { "StepId" });
            DropIndex("dbo.T_Prog_BizProgress", new[] { "ItemId" });
            DropIndex("dbo.T_Prog_BizProgress", new[] { "CateId" });
            DropIndex("dbo.T_Prog_BizProgress", new[] { "CusId" });
            DropIndex("dbo.T_Prog_BizItems", new[] { "CateId" });
            DropTable("dbo.T_Prog_CustomerCost");
            DropTable("dbo.T_Prog_ProgressImage");
            DropTable("dbo.T_Prog_BizPromotion");
            DropTable("dbo.T_Prog_Customers");
            DropTable("dbo.T_Prog_BizSteps");
            DropTable("dbo.T_Prog_BizProgress");
            DropTable("dbo.T_Prog_BizItems");
            DropTable("dbo.T_Prog_BizCategory");
        }
    }
}
