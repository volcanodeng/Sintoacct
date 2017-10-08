namespace Sintoacct.Progress.Models.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Sintoacct.Progress.Models.BizProgressContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Sintoacct.Progress.Models.BizProgressContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.BizCategories.AddOrUpdate(
                new BizCategory { CategoryName = "工商业务", SortIndex = 1 },
                new BizCategory { CategoryName = "代理记账", SortIndex = 2 },
                new BizCategory { CategoryName = "资质代办", SortIndex = 3 },
                new BizCategory { CategoryName = "咨询顾问", SortIndex = 4 },
                new BizCategory { CategoryName = "其他业务", SortIndex = 5 }
                );

            context.BizItems.AddOrUpdate(
                new BizItems { ItemName= "有限责任公司注册" ,SortIndex=1,CateId=1},
                new BizItems { ItemName= "分公司注册" ,SortIndex=2,CateId=1},
                new BizItems { ItemName = "个体户注册", SortIndex = 3, CateId = 1 },
                new BizItems { ItemName = "股份公司注册", SortIndex = 4, CateId = 1 },
                new BizItems { ItemName = "公司注销", SortIndex = 5, CateId = 1 },
                new BizItems { ItemName = "个体户注销", SortIndex = 6, CateId = 1 },
                new BizItems { ItemName = "公司改名", SortIndex = 7, CateId = 1 },
                new BizItems { ItemName = "增加或减少注册资本", SortIndex = 8, CateId = 1 },
                new BizItems { ItemName = "股东变更", SortIndex = 9, CateId = 1 },
                new BizItems { ItemName = "经营地址变更（同城区）", SortIndex = 10, CateId = 1 },
                new BizItems { ItemName = "经营地址变更（跨城区）", SortIndex = 11, CateId = 1 },
                new BizItems { ItemName = "法定代表人变更", SortIndex = 12, CateId = 1 },
                new BizItems { ItemName = "董事或监事变更", SortIndex = 13, CateId = 1 },
                new BizItems { ItemName = "企业信息公示（工商年报）", SortIndex = 14, CateId = 1 },
                new BizItems { ItemName = "公司三证合一（五证合一）", SortIndex = 15, CateId = 1 },
                new BizItems { ItemName = "个体户两证合一", SortIndex = 16, CateId = 1 },
                new BizItems { ItemName = "工商异常移出", SortIndex = 17, CateId = 1 },
                new BizItems { ItemName = "工商查档", SortIndex = 18, CateId = 1 },
                new BizItems { ItemName = "其他业务", SortIndex = 99, CateId = 1 },

                new BizItems { ItemName = "个体户代理记账", SortIndex = 1, CateId = 2 },
                new BizItems { ItemName = "小规模纳税人代理记账", SortIndex = 2, CateId = 2 },
                new BizItems { ItemName = "一般纳税人代理记账", SortIndex = 3, CateId = 2 },
                new BizItems { ItemName = "账簿、凭证等耗材费", SortIndex = 4, CateId = 2 },
                new BizItems { ItemName = "申请或领购发票", SortIndex = 5, CateId = 2 },
                new BizItems { ItemName = "发票增量", SortIndex = 6, CateId = 2 },
                new BizItems { ItemName = "发票升位", SortIndex = 7, CateId = 2 },
                new BizItems { ItemName = "银行对账单、回执单打印", SortIndex = 8, CateId = 2 },
                new BizItems { ItemName = "一般纳税人认定（新设立公司）", SortIndex = 9, CateId = 2 },
                new BizItems { ItemName = "一般纳税人认定（存续公司）", SortIndex = 10, CateId = 2 },
                new BizItems { ItemName = "税务机关代开发票", SortIndex = 11, CateId = 2 },
                new BizItems { ItemName = "税控盘购买及发行", SortIndex = 12, CateId = 2 },
                new BizItems { ItemName = "代理发票开具", SortIndex = 13, CateId = 2 },
                new BizItems { ItemName = "清理乱账、调整乱账", SortIndex = 14, CateId = 2 },
                new BizItems { ItemName = "内部账务核算", SortIndex = 15, CateId = 2 },
                new BizItems { ItemName = "办理银行基本账户或一般户开立", SortIndex = 16, CateId = 2 },
                new BizItems { ItemName = "办理银行基本账户或一般户撤销", SortIndex = 17, CateId = 2 },
                new BizItems { ItemName = "银行开户信息变更", SortIndex = 18, CateId = 2 },
                new BizItems { ItemName = "办理国地税三方协议扣款", SortIndex = 19, CateId = 2 },
                new BizItems { ItemName = "增值税进项发票认证", SortIndex = 20, CateId = 2 },
                new BizItems { ItemName = "税控开票系统安装及使用培训", SortIndex = 21, CateId = 2 },
                new BizItems { ItemName = "税控盘现场清卡或解锁", SortIndex = 22, CateId = 2 },
                new BizItems { ItemName = "企业所得税汇算清缴", SortIndex = 23, CateId = 2 },
                new BizItems { ItemName = "往期未按期申报纳税处理", SortIndex = 24, CateId = 2 },
                new BizItems { ItemName = "残疾人就业保障金核定及缴纳", SortIndex = 25, CateId = 2 },
                new BizItems { ItemName = "业务承接前账务处理", SortIndex = 26, CateId = 2 },

                new BizItems { ItemName = "食品经营许可证", SortIndex = 1, CateId = 3 },
                new BizItems { ItemName = "公共场所卫生许可证", SortIndex = 2, CateId = 3 },
                new BizItems { ItemName = "网络文化经营许可证", SortIndex = 3, CateId = 3 },
                new BizItems { ItemName = "烟草专卖零售许可证", SortIndex = 4, CateId = 3 },
                new BizItems { ItemName = "医疗器械经营备案（二类）", SortIndex = 5, CateId = 3 },
                new BizItems { ItemName = "医疗器械经营许可证（三类）", SortIndex = 6, CateId = 3 },
                new BizItems { ItemName = "道路运输许可证", SortIndex = 7, CateId = 3 },
                new BizItems { ItemName = "劳务派遣资质", SortIndex = 8, CateId = 3 },
                new BizItems { ItemName = "施工总承包等建筑类资质", SortIndex = 9, CateId = 3 },

                new BizItems { ItemName = "企业管理顾问", SortIndex = 1, CateId = 4 },
                new BizItems { ItemName = "融资咨询", SortIndex = 2, CateId = 4 },
                new BizItems { ItemName = "税务咨询", SortIndex = 3, CateId = 4 },
                new BizItems { ItemName = "审计咨询", SortIndex = 4, CateId = 4 },
                new BizItems { ItemName = "评估咨询", SortIndex = 5, CateId = 4 },
                new BizItems { ItemName = "税收筹划", SortIndex = 6, CateId = 4 },
                new BizItems { ItemName = "企业培训（企业内训）", SortIndex = 7, CateId = 4 },
                new BizItems { ItemName = "企业培训（新拓组织的集中培训）", SortIndex = 8, CateId = 4 },
                new BizItems { ItemName = "企业内部控制设计", SortIndex = 9, CateId = 4 },
                new BizItems { ItemName = "会计信息化咨询与实施", SortIndex = 10, CateId = 4 },
                new BizItems { ItemName = "财务管理体系构建", SortIndex = 11, CateId = 4 },
                new BizItems { ItemName = "企业诊断", SortIndex = 12, CateId = 4 },
                new BizItems { ItemName = "企业战略规划咨询", SortIndex = 13, CateId = 4 },
                new BizItems { ItemName = "企业并购重组顾问", SortIndex = 14, CateId = 4 },
                new BizItems { ItemName = "企业尽职调查", SortIndex = 15, CateId = 4 },

                new BizItems { ItemName = "其他业务", SortIndex = 99, CateId = 5 }
                );

            context.BizSteps.AddOrUpdate(
                new BizSteps { StepName = "签订合同（口头协议）", SortIndex = 1, CateId = 1 },
                new BizSteps { StepName = "任务分派", SortIndex = 1, CateId = 1 },
                new BizSteps { StepName = "资料收取", SortIndex = 1, CateId = 1 },
                new BizSteps { StepName = "核名", SortIndex = 1, CateId = 1 },
                new BizSteps { StepName = "工商移出异常", SortIndex = 1, CateId = 1 },
                new BizSteps { StepName = "工商档案迁移", SortIndex = 1, CateId = 1 },
                new BizSteps { StepName = "提交设立", SortIndex = 1, CateId = 1 },
                new BizSteps { StepName = "领取营业执照", SortIndex = 1, CateId = 1 },
                new BizSteps { StepName = "印章刻制", SortIndex = 1, CateId = 1 },
                new BizSteps { StepName = "领取印章", SortIndex = 1, CateId = 1 },
                new BizSteps { StepName = "工商查档", SortIndex = 1, CateId = 1 },
                new BizSteps { StepName = "税局信息采集", SortIndex = 1, CateId = 1 },
                new BizSteps { StepName = "国税异常处理", SortIndex = 1, CateId = 1 },
                new BizSteps { StepName = "地税异常处理", SortIndex = 1, CateId = 1 },
                new BizSteps { StepName = "国税备案", SortIndex = 1, CateId = 1 },
                new BizSteps { StepName = "地税备案", SortIndex = 1, CateId = 1 },
                new BizSteps { StepName = "银行对公户信息变更", SortIndex = 1, CateId = 1 },
                new BizSteps { StepName = "三方协议扣款重签", SortIndex = 1, CateId = 1 },
                new BizSteps { StepName = "发票验旧及税盘重发行", SortIndex = 1, CateId = 1 },
                new BizSteps { StepName = "代垫费用", SortIndex = 1, CateId = 1 },
                new BizSteps { StepName = "费用对账", SortIndex = 1, CateId = 1 },
                new BizSteps { StepName = "费用收取", SortIndex = 1, CateId = 1 },
                new BizSteps { StepName = "资料归档", SortIndex = 1, CateId = 1 },
                new BizSteps { StepName = "资料归还", SortIndex = 1, CateId = 1 },
                new BizSteps { StepName = "客户回访", SortIndex = 1, CateId = 1 },
                new BizSteps { StepName = "工商年报", SortIndex = 1, CateId = 1 },
                new BizSteps { StepName = "沟通记录", SortIndex = 1, CateId = 1 },

                new BizSteps { StepName = "签订合同（口头协议）", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "任务分派", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "资料收取", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "会计凭证提取", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "原始凭证整理", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "会计凭证录入", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "账务处理", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "报表编制", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "凭证打印及装订", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "账簿打印及装订", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "报表打印及装订", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "国税申报", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "缴纳国税税款", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "地税申报", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "缴纳地税税款", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "企业所得税汇算清缴", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "国税纳税申报表打印及装订", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "地税纳税申报表打印及装订", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "账套备份", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "报表、纳税申报表电子档存档", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "代垫费用", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "税控盘领购及发行", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "发票申请", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "发票增量申请提交", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "发票增量申请审批进度查询", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "发票增量审批结果", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "发票升位申请提交", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "发票升位申请审批进度查询", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "发票升位审批结果", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "对公户开立（撤销）资料提交", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "对公户开立（撤销）人行审批", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "对公户开户许可证领取（撤销）", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "对公户网银开通", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "国税打印三方协议扣款", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "地市打印三方协议扣款", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "银行签订三方协议扣款", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "三方协议扣款交回国税", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "三方协议扣款交回地税", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "残疾人就业保障金核定及缴纳", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "发票开具", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "税控盘现场清卡或解锁", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "一般纳税人认定资料提交", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "费用对账", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "费用收取", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "沟通记录", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "客服回访", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "资料归还", SortIndex = 1, CateId = 2 },

                new BizSteps { StepName = "签订合同（口头协议）", SortIndex = 1, CateId = 3 },
                new BizSteps { StepName = "任务分派", SortIndex = 1, CateId = 3 },
                new BizSteps { StepName = "资料收取", SortIndex = 1, CateId = 3 },
                new BizSteps { StepName = "申请材料准备", SortIndex = 1, CateId = 3 },
                new BizSteps { StepName = "申请材料提交", SortIndex = 1, CateId = 3 },
                new BizSteps { StepName = "申请材料审批进度查询", SortIndex = 1, CateId = 3 },
                new BizSteps { StepName = "申请涉及费用缴纳", SortIndex = 1, CateId = 3 },
                new BizSteps { StepName = "整改要求", SortIndex = 1, CateId = 3 },
                new BizSteps { StepName = "整改材料提交", SortIndex = 1, CateId = 3 },
                new BizSteps { StepName = "领取证件", SortIndex = 1, CateId = 3 },
                new BizSteps { StepName = "费用对账", SortIndex = 1, CateId = 3 },
                new BizSteps { StepName = "沟通记录", SortIndex = 1, CateId = 3 },
                new BizSteps { StepName = "费用收取", SortIndex = 1, CateId = 3 },
                new BizSteps { StepName = "资料归还", SortIndex = 1, CateId = 3 },
                new BizSteps { StepName = "客户回访", SortIndex = 1, CateId = 3 },

                new BizSteps { StepName = "签订合同（口头协议）", SortIndex = 1, CateId = 4 },
                new BizSteps { StepName = "任务分派", SortIndex = 1, CateId = 4 },
                new BizSteps { StepName = "资料收取", SortIndex = 1, CateId = 4 },
                new BizSteps { StepName = "沟通记录", SortIndex = 1, CateId = 4 },
                new BizSteps { StepName = "结果提交", SortIndex = 1, CateId = 4 },
                new BizSteps { StepName = "结果反馈及修订", SortIndex = 1, CateId = 4 },
                new BizSteps { StepName = "最终版报告提交", SortIndex = 1, CateId = 4 },
                new BizSteps { StepName = "费用对账", SortIndex = 1, CateId = 4 },
                new BizSteps { StepName = "费用收取", SortIndex = 1, CateId = 4 },
                new BizSteps { StepName = "资料归还", SortIndex = 1, CateId = 4 },
                new BizSteps { StepName = "客户回访", SortIndex = 1, CateId = 4 },

                new BizSteps { StepName = "签订合同（口头协议）", SortIndex = 1, CateId = 5 },
                new BizSteps { StepName = "任务分派", SortIndex = 1, CateId = 5 },
                new BizSteps { StepName = "资料收取", SortIndex = 1, CateId = 5 },
                new BizSteps { StepName = "沟通记录", SortIndex = 1, CateId = 5 },
                new BizSteps { StepName = "结果提交", SortIndex = 1, CateId = 5 },
                new BizSteps { StepName = "结果反馈及修订", SortIndex = 1, CateId = 5 },
                new BizSteps { StepName = "最终版报告提交", SortIndex = 1, CateId = 5 },
                new BizSteps { StepName = "费用对账", SortIndex = 1, CateId = 5 },
                new BizSteps { StepName = "费用收取", SortIndex = 1, CateId = 5 },
                new BizSteps { StepName = "资料归还", SortIndex = 1, CateId = 5 },
                new BizSteps { StepName = "客户回访", SortIndex = 1, CateId = 5 }

                );
        }
    }
}
