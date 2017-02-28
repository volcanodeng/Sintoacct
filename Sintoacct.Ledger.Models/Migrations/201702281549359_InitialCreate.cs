namespace Sintoacct.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.T_Abstract_Temp",
                c => new
                    {
                        AbsId = c.Int(nullable: false, identity: true),
                        Abstract = c.String(nullable: false, maxLength: 200),
                        AbId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.AbsId)
                .ForeignKey("dbo.T_Account_Book", t => t.AbId, cascadeDelete: true)
                .Index(t => t.AbId);
            
            CreateTable(
                "dbo.T_Account_Book",
                c => new
                    {
                        AbId = c.Guid(nullable: false, identity: true),
                        Currency = c.String(maxLength: 10),
                        StartYear = c.Int(nullable: false),
                        StartPeriod = c.Int(nullable: false),
                        FiscalSystem = c.Int(nullable: false),
                        ComId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AbId)
                .ForeignKey("dbo.T_Company", t => t.ComId, cascadeDelete: true)
                .Index(t => t.ComId);
            
            CreateTable(
                "dbo.T_Account",
                c => new
                    {
                        AccId = c.Long(nullable: false, identity: true),
                        AccCode = c.String(nullable: false, maxLength: 20),
                        ParentAccCode = c.String(maxLength: 20),
                        AcId = c.Int(nullable: false),
                        AccName = c.String(nullable: false, maxLength: 50),
                        Direction = c.String(nullable: false, maxLength: 5),
                        IsAuxiliary = c.Boolean(nullable: false),
                        AuxTypeIds = c.String(maxLength: 200),
                        AuxTypeNames = c.String(maxLength: 200),
                        IsQuantity = c.Boolean(nullable: false),
                        Unit = c.String(maxLength: 5),
                        InitialQuantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InitialBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        YtdDebitQuantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        YtdDebit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        YtdCreditQuantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        YtdCredit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        YtdBeginBalanceQuantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        YtdBeginBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        State = c.Int(nullable: false),
                        AbId = c.Guid(nullable: false),
                        Creator = c.String(nullable: false, maxLength: 50),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AccId)
                .ForeignKey("dbo.T_Account_Book", t => t.AbId, cascadeDelete: true)
                .ForeignKey("dbo.T_Account_Category", t => t.AcId, cascadeDelete: true)
                .Index(t => t.AcId)
                .Index(t => t.AbId);
            
            CreateTable(
                "dbo.T_Account_Category",
                c => new
                    {
                        AcId = c.Int(nullable: false, identity: true),
                        ParentAcId = c.Int(nullable: false),
                        CategoryName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.AcId);
            
            CreateTable(
                "dbo.T_Auxiliary",
                c => new
                    {
                        AuxId = c.Long(nullable: false, identity: true),
                        AuxCode = c.String(maxLength: 20),
                        AuxName = c.String(maxLength: 20),
                        AuxiliaryState = c.Int(nullable: false),
                        AtId = c.Int(nullable: false),
                        AbId = c.Guid(nullable: false),
                        Creator = c.String(maxLength: 50),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AuxId)
                .ForeignKey("dbo.T_Account_Book", t => t.AbId, cascadeDelete: true)
                .ForeignKey("dbo.T_Auxiliary_Type", t => t.AtId, cascadeDelete: true)
                .Index(t => t.AtId)
                .Index(t => t.AbId);
            
            CreateTable(
                "dbo.T_Auxiliary_Type",
                c => new
                    {
                        AtId = c.Int(nullable: false, identity: true),
                        AuxType = c.String(maxLength: 20),
                        AbId = c.Guid(),
                    })
                .PrimaryKey(t => t.AtId)
                .ForeignKey("dbo.T_Account_Book", t => t.AbId)
                .Index(t => t.AbId);
            
            CreateTable(
                "dbo.T_Company",
                c => new
                    {
                        ComId = c.Int(nullable: false, identity: true),
                        ComName = c.String(nullable: false, maxLength: 50),
                        ComShortName = c.String(maxLength: 50),
                        ComAddress = c.String(maxLength: 100),
                        LegalRepresentative = c.String(maxLength: 10),
                        Mobile = c.String(maxLength: 30),
                        RegionCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ComId)
                .ForeignKey("dbo.T_Region", t => t.RegionCode, cascadeDelete: true)
                .Index(t => t.RegionCode);
            
            CreateTable(
                "dbo.T_Region",
                c => new
                    {
                        RegionCode = c.Int(nullable: false, identity: true),
                        ProvinceCode = c.Int(nullable: false),
                        CityCode = c.Int(nullable: false),
                        RegionName = c.String(nullable: false, maxLength: 20),
                        ProvinceName = c.String(nullable: false, maxLength: 20),
                        CityName = c.String(nullable: false, maxLength: 20),
                        Pinyin = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.RegionCode);
            
            CreateTable(
                "dbo.T_User_Book",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        AbId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.AbId })
                .ForeignKey("dbo.T_Account_Book", t => t.AbId, cascadeDelete: true)
                .Index(t => t.AbId);
            
            CreateTable(
                "dbo.T_Voucher_Detail_Template",
                c => new
                    {
                        VdtId = c.Long(nullable: false, identity: true),
                        Abstract = c.String(nullable: false, maxLength: 200),
                        AccountCode = c.String(nullable: false, maxLength: 100),
                        AccountName = c.String(nullable: false, maxLength: 100),
                        Quantity = c.Decimal(precision: 18, scale: 2),
                        Price = c.Decimal(precision: 18, scale: 2),
                        Debit = c.Decimal(precision: 18, scale: 2),
                        Credit = c.Decimal(precision: 18, scale: 2),
                        AbId = c.Guid(nullable: false),
                        Creator = c.String(nullable: false, maxLength: 50),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.VdtId)
                .ForeignKey("dbo.T_Account_Book", t => t.AbId, cascadeDelete: true)
                .Index(t => t.AbId);
            
            CreateTable(
                "dbo.T_Voucher",
                c => new
                    {
                        VId = c.Long(nullable: false, identity: true),
                        CertWordSN = c.Int(nullable: false),
                        VoucherDate = c.DateTime(nullable: false),
                        PaymentTerms = c.String(nullable: false, maxLength: 20),
                        InvoiceCount = c.Int(nullable: false),
                        State = c.Int(nullable: false),
                        AbId = c.Guid(nullable: false),
                        Creator = c.String(nullable: false, maxLength: 50),
                        CreateTime = c.DateTime(nullable: false),
                        Review = c.String(),
                        ReviewTime = c.DateTime(),
                        CertificateWord_CwId = c.Int(),
                    })
                .PrimaryKey(t => t.VId)
                .ForeignKey("dbo.T_Account_Book", t => t.AbId, cascadeDelete: true)
                .ForeignKey("dbo.T_Certificate_Word", t => t.CertificateWord_CwId)
                .Index(t => t.AbId)
                .Index(t => t.CertificateWord_CwId);
            
            CreateTable(
                "dbo.T_Certificate_Word",
                c => new
                    {
                        CwId = c.Int(nullable: false, identity: true),
                        CertWord = c.String(nullable: false, maxLength: 50),
                        PrintTitle = c.String(maxLength: 50),
                        IsDefault = c.Boolean(nullable: false),
                        AbId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.CwId)
                .ForeignKey("dbo.T_Account_Book", t => t.AbId, cascadeDelete: true)
                .Index(t => t.AbId);
            
            CreateTable(
                "dbo.T_Voucher_Detail",
                c => new
                    {
                        VdId = c.Long(nullable: false, identity: true),
                        VId = c.Long(nullable: false),
                        Abstract = c.String(nullable: false, maxLength: 200),
                        AccountCode = c.String(maxLength: 100),
                        AccountName = c.String(maxLength: 100),
                        Quantity = c.Decimal(precision: 18, scale: 2),
                        Price = c.Decimal(precision: 18, scale: 2),
                        Debit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Credit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Account_AccId = c.Long(),
                    })
                .PrimaryKey(t => t.VdId)
                .ForeignKey("dbo.T_Account", t => t.Account_AccId)
                .ForeignKey("dbo.T_Voucher", t => t.VId, cascadeDelete: true)
                .Index(t => t.VId)
                .Index(t => t.Account_AccId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.T_Abstract_Temp", "AbId", "dbo.T_Account_Book");
            DropForeignKey("dbo.T_Voucher_Detail", "VId", "dbo.T_Voucher");
            DropForeignKey("dbo.T_Voucher_Detail", "Account_AccId", "dbo.T_Account");
            DropForeignKey("dbo.T_Voucher", "CertificateWord_CwId", "dbo.T_Certificate_Word");
            DropForeignKey("dbo.T_Certificate_Word", "AbId", "dbo.T_Account_Book");
            DropForeignKey("dbo.T_Voucher", "AbId", "dbo.T_Account_Book");
            DropForeignKey("dbo.T_Voucher_Detail_Template", "AbId", "dbo.T_Account_Book");
            DropForeignKey("dbo.T_User_Book", "AbId", "dbo.T_Account_Book");
            DropForeignKey("dbo.T_Account_Book", "ComId", "dbo.T_Company");
            DropForeignKey("dbo.T_Company", "RegionCode", "dbo.T_Region");
            DropForeignKey("dbo.T_Auxiliary", "AtId", "dbo.T_Auxiliary_Type");
            DropForeignKey("dbo.T_Auxiliary_Type", "AbId", "dbo.T_Account_Book");
            DropForeignKey("dbo.T_Auxiliary", "AbId", "dbo.T_Account_Book");
            DropForeignKey("dbo.T_Account", "AcId", "dbo.T_Account_Category");
            DropForeignKey("dbo.T_Account", "AbId", "dbo.T_Account_Book");
            DropIndex("dbo.T_Voucher_Detail", new[] { "Account_AccId" });
            DropIndex("dbo.T_Voucher_Detail", new[] { "VId" });
            DropIndex("dbo.T_Certificate_Word", new[] { "AbId" });
            DropIndex("dbo.T_Voucher", new[] { "CertificateWord_CwId" });
            DropIndex("dbo.T_Voucher", new[] { "AbId" });
            DropIndex("dbo.T_Voucher_Detail_Template", new[] { "AbId" });
            DropIndex("dbo.T_User_Book", new[] { "AbId" });
            DropIndex("dbo.T_Company", new[] { "RegionCode" });
            DropIndex("dbo.T_Auxiliary_Type", new[] { "AbId" });
            DropIndex("dbo.T_Auxiliary", new[] { "AbId" });
            DropIndex("dbo.T_Auxiliary", new[] { "AtId" });
            DropIndex("dbo.T_Account", new[] { "AbId" });
            DropIndex("dbo.T_Account", new[] { "AcId" });
            DropIndex("dbo.T_Account_Book", new[] { "ComId" });
            DropIndex("dbo.T_Abstract_Temp", new[] { "AbId" });
            DropTable("dbo.T_Voucher_Detail");
            DropTable("dbo.T_Certificate_Word");
            DropTable("dbo.T_Voucher");
            DropTable("dbo.T_Voucher_Detail_Template");
            DropTable("dbo.T_User_Book");
            DropTable("dbo.T_Region");
            DropTable("dbo.T_Company");
            DropTable("dbo.T_Auxiliary_Type");
            DropTable("dbo.T_Auxiliary");
            DropTable("dbo.T_Account_Category");
            DropTable("dbo.T_Account");
            DropTable("dbo.T_Account_Book");
            DropTable("dbo.T_Abstract_Temp");
        }
    }
}
