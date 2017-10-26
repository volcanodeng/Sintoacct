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
                        ServicePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItemId)
                .ForeignKey("dbo.T_Prog_BizCategory", t => t.CateId, cascadeDelete: true)
                .Index(t => t.CateId);
            
            CreateTable(
                "dbo.T_Prog_BizSteps",
                c => new
                    {
                        StepId = c.Int(nullable: false, identity: true),
                        StepName = c.String(maxLength: 50),
                        SortIndex = c.Int(nullable: false),
                        ItemId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StepId)
                .ForeignKey("dbo.T_Prog_BizItems", t => t.ItemId, cascadeDelete: true)
                .Index(t => t.ItemId);
            
            CreateTable(
                "dbo.T_Prog_WorkProgress",
                c => new
                    {
                        ProgId = c.Long(nullable: false, identity: true),
                        WoId = c.Long(nullable: false),
                        ItemId = c.Int(nullable: false),
                        StepId = c.Int(nullable: false),
                        CompletedTime = c.DateTime(),
                        ResultDesc = c.String(maxLength: 100),
                        AdvanceExpenditure = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Creator = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ProgId)
                .ForeignKey("dbo.T_Prog_BizItems", t => t.ItemId, cascadeDelete: true)
                .ForeignKey("dbo.T_Prog_BizSteps", t => t.StepId)
                .ForeignKey("dbo.T_Prog_WorkOrder", t => t.WoId, cascadeDelete: true)
                .Index(t => t.WoId)
                .Index(t => t.ItemId)
                .Index(t => t.StepId);
            
            CreateTable(
                "dbo.T_Prog_ProgressImage",
                c => new
                    {
                        ImgId = c.Long(nullable: false, identity: true),
                        OriginalImageName = c.String(),
                        ServerImageName = c.String(),
                        ProgId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ImgId)
                .ForeignKey("dbo.T_Prog_WorkProgress", t => t.ProgId, cascadeDelete: true)
                .Index(t => t.ProgId);
            
            CreateTable(
                "dbo.T_Prog_WorkOrder",
                c => new
                    {
                        WoId = c.Long(nullable: false, identity: true),
                        CusId = c.Long(nullable: false),
                        ContractTime = c.DateTime(nullable: false),
                        Remark = c.String(),
                        BizManager = c.String(maxLength: 50),
                        BizOperations = c.String(maxLength: 100),
                        Recommend = c.String(),
                        CommercialExpense = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PreferentialAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AdvanceExpenditure = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AmountReceived = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Creator = c.String(maxLength: 50),
                        CreateTime = c.DateTime(nullable: false),
                        State = c.Int(nullable: false),
                        BizItems_ItemId = c.Int(),
                    })
                .PrimaryKey(t => t.WoId)
                .ForeignKey("dbo.T_Prog_Customers", t => t.CusId, cascadeDelete: true)
                .ForeignKey("dbo.T_Prog_BizItems", t => t.BizItems_ItemId)
                .Index(t => t.CusId)
                .Index(t => t.BizItems_ItemId);
            
            CreateTable(
                "dbo.T_Prog_Customers",
                c => new
                    {
                        CusId = c.Long(nullable: false, identity: true),
                        CustomerName = c.String(maxLength: 50),
                        CustomerAddress = c.String(maxLength: 200),
                        BusinessAddress = c.String(maxLength: 200),
                        Contacts = c.String(maxLength: 50),
                        Phone = c.String(nullable: false, maxLength: 20),
                        Level = c.Int(),
                        PromId = c.String(maxLength: 50),
                        PromName = c.String(maxLength: 50),
                        State = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CusId);
            
            CreateTable(
                "dbo.T_Prog_WorkOrderItem",
                c => new
                    {
                        WoId = c.Long(nullable: false),
                        ItemId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.WoId, t.ItemId })
                .ForeignKey("dbo.T_Prog_BizItems", t => t.ItemId, cascadeDelete: true)
                .ForeignKey("dbo.T_Prog_WorkOrder", t => t.WoId, cascadeDelete: true)
                .Index(t => t.WoId)
                .Index(t => t.ItemId);
            
            CreateTable(
                "dbo.T_Prog_WorkOrderPayment",
                c => new
                    {
                        PayId = c.Long(nullable: false, identity: true),
                        WoId = c.Long(nullable: false),
                        AmountReceived = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CollectionDays = c.String(maxLength: 100),
                        CreditingType = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.PayId)
                .ForeignKey("dbo.T_Prog_WorkOrder", t => t.WoId, cascadeDelete: true)
                .Index(t => t.WoId);
            
            CreateTable(
                "dbo.T_Prog_BizPromotion",
                c => new
                    {
                        PromId = c.Long(nullable: false, identity: true),
                        ParentPromId = c.Long(),
                        OpName = c.String(maxLength: 50),
                        WeixinOpenId = c.String(),
                        PromLevel = c.Int(nullable: false),
                        PromChain = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.PromId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.T_Prog_WorkOrder", "BizItems_ItemId", "dbo.T_Prog_BizItems");
            DropForeignKey("dbo.T_Prog_WorkProgress", "WoId", "dbo.T_Prog_WorkOrder");
            DropForeignKey("dbo.T_Prog_WorkOrderPayment", "WoId", "dbo.T_Prog_WorkOrder");
            DropForeignKey("dbo.T_Prog_WorkOrderItem", "WoId", "dbo.T_Prog_WorkOrder");
            DropForeignKey("dbo.T_Prog_WorkOrderItem", "ItemId", "dbo.T_Prog_BizItems");
            DropForeignKey("dbo.T_Prog_WorkOrder", "CusId", "dbo.T_Prog_Customers");
            DropForeignKey("dbo.T_Prog_ProgressImage", "ProgId", "dbo.T_Prog_WorkProgress");
            DropForeignKey("dbo.T_Prog_WorkProgress", "StepId", "dbo.T_Prog_BizSteps");
            DropForeignKey("dbo.T_Prog_WorkProgress", "ItemId", "dbo.T_Prog_BizItems");
            DropForeignKey("dbo.T_Prog_BizSteps", "ItemId", "dbo.T_Prog_BizItems");
            DropForeignKey("dbo.T_Prog_BizItems", "CateId", "dbo.T_Prog_BizCategory");
            DropIndex("dbo.T_Prog_WorkOrderPayment", new[] { "WoId" });
            DropIndex("dbo.T_Prog_WorkOrderItem", new[] { "ItemId" });
            DropIndex("dbo.T_Prog_WorkOrderItem", new[] { "WoId" });
            DropIndex("dbo.T_Prog_WorkOrder", new[] { "BizItems_ItemId" });
            DropIndex("dbo.T_Prog_WorkOrder", new[] { "CusId" });
            DropIndex("dbo.T_Prog_ProgressImage", new[] { "ProgId" });
            DropIndex("dbo.T_Prog_WorkProgress", new[] { "StepId" });
            DropIndex("dbo.T_Prog_WorkProgress", new[] { "ItemId" });
            DropIndex("dbo.T_Prog_WorkProgress", new[] { "WoId" });
            DropIndex("dbo.T_Prog_BizSteps", new[] { "ItemId" });
            DropIndex("dbo.T_Prog_BizItems", new[] { "CateId" });
            DropTable("dbo.T_Prog_BizPromotion");
            DropTable("dbo.T_Prog_WorkOrderPayment");
            DropTable("dbo.T_Prog_WorkOrderItem");
            DropTable("dbo.T_Prog_Customers");
            DropTable("dbo.T_Prog_WorkOrder");
            DropTable("dbo.T_Prog_ProgressImage");
            DropTable("dbo.T_Prog_WorkProgress");
            DropTable("dbo.T_Prog_BizSteps");
            DropTable("dbo.T_Prog_BizItems");
            DropTable("dbo.T_Prog_BizCategory");
        }
    }
}
