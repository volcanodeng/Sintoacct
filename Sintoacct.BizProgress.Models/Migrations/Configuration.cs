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
                new BizCategory { CategoryName = "����ҵ��", SortIndex = 1 },
                new BizCategory { CategoryName = "�������", SortIndex = 2 },
                new BizCategory { CategoryName = "���ʴ���", SortIndex = 3 },
                new BizCategory { CategoryName = "��ѯ����", SortIndex = 4 },
                new BizCategory { CategoryName = "����ҵ��", SortIndex = 5 }
                );

            context.BizItems.AddOrUpdate(
                new BizItems { ItemName= "�������ι�˾ע��" ,SortIndex=1,CateId=1},
                new BizItems { ItemName= "�ֹ�˾ע��" ,SortIndex=2,CateId=1}
                );
        }
    }
}
