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
                new BizItems { ItemName= "分公司注册" ,SortIndex=2,CateId=1}
                );
        }
    }
}
