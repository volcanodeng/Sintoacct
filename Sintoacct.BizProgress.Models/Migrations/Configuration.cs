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
                new BizItems { ItemName= "�ֹ�˾ע��" ,SortIndex=2,CateId=1},
                new BizItems { ItemName = "���廧ע��", SortIndex = 3, CateId = 1 },
                new BizItems { ItemName = "�ɷݹ�˾ע��", SortIndex = 4, CateId = 1 },
                new BizItems { ItemName = "��˾ע��", SortIndex = 5, CateId = 1 },
                new BizItems { ItemName = "���廧ע��", SortIndex = 6, CateId = 1 },
                new BizItems { ItemName = "��˾����", SortIndex = 7, CateId = 1 },
                new BizItems { ItemName = "���ӻ����ע���ʱ�", SortIndex = 8, CateId = 1 },
                new BizItems { ItemName = "�ɶ����", SortIndex = 9, CateId = 1 },
                new BizItems { ItemName = "��Ӫ��ַ�����ͬ������", SortIndex = 10, CateId = 1 },
                new BizItems { ItemName = "��Ӫ��ַ������������", SortIndex = 11, CateId = 1 },
                new BizItems { ItemName = "���������˱��", SortIndex = 12, CateId = 1 },
                new BizItems { ItemName = "���»���±��", SortIndex = 13, CateId = 1 },
                new BizItems { ItemName = "��ҵ��Ϣ��ʾ�������걨��", SortIndex = 14, CateId = 1 },
                new BizItems { ItemName = "��˾��֤��һ����֤��һ��", SortIndex = 15, CateId = 1 },
                new BizItems { ItemName = "���廧��֤��һ", SortIndex = 16, CateId = 1 },
                new BizItems { ItemName = "�����쳣�Ƴ�", SortIndex = 17, CateId = 1 },
                new BizItems { ItemName = "���̲鵵", SortIndex = 18, CateId = 1 },
                new BizItems { ItemName = "����ҵ��", SortIndex = 99, CateId = 1 },

                new BizItems { ItemName = "���廧�������", SortIndex = 1, CateId = 2 },
                new BizItems { ItemName = "С��ģ��˰�˴������", SortIndex = 2, CateId = 2 },
                new BizItems { ItemName = "һ����˰�˴������", SortIndex = 3, CateId = 2 },
                new BizItems { ItemName = "�˲���ƾ֤�ȺĲķ�", SortIndex = 4, CateId = 2 },
                new BizItems { ItemName = "������칺��Ʊ", SortIndex = 5, CateId = 2 },
                new BizItems { ItemName = "��Ʊ����", SortIndex = 6, CateId = 2 },
                new BizItems { ItemName = "��Ʊ��λ", SortIndex = 7, CateId = 2 },
                new BizItems { ItemName = "���ж��˵�����ִ����ӡ", SortIndex = 8, CateId = 2 },
                new BizItems { ItemName = "һ����˰���϶�����������˾��", SortIndex = 9, CateId = 2 },
                new BizItems { ItemName = "һ����˰���϶���������˾��", SortIndex = 10, CateId = 2 },
                new BizItems { ItemName = "˰����ش�����Ʊ", SortIndex = 11, CateId = 2 },
                new BizItems { ItemName = "˰���̹��򼰷���", SortIndex = 12, CateId = 2 },
                new BizItems { ItemName = "����Ʊ����", SortIndex = 13, CateId = 2 },
                new BizItems { ItemName = "�������ˡ���������", SortIndex = 14, CateId = 2 },
                new BizItems { ItemName = "�ڲ��������", SortIndex = 15, CateId = 2 },
                new BizItems { ItemName = "�������л����˻���һ�㻧����", SortIndex = 16, CateId = 2 },
                new BizItems { ItemName = "�������л����˻���һ�㻧����", SortIndex = 17, CateId = 2 },
                new BizItems { ItemName = "���п�����Ϣ���", SortIndex = 18, CateId = 2 },
                new BizItems { ItemName = "�������˰����Э��ۿ�", SortIndex = 19, CateId = 2 },
                new BizItems { ItemName = "��ֵ˰���Ʊ��֤", SortIndex = 20, CateId = 2 },
                new BizItems { ItemName = "˰�ؿ�Ʊϵͳ��װ��ʹ����ѵ", SortIndex = 21, CateId = 2 },
                new BizItems { ItemName = "˰�����ֳ��忨�����", SortIndex = 22, CateId = 2 },
                new BizItems { ItemName = "��ҵ����˰�������", SortIndex = 23, CateId = 2 },
                new BizItems { ItemName = "����δ�����걨��˰����", SortIndex = 24, CateId = 2 },
                new BizItems { ItemName = "�м��˾�ҵ���Ͻ�˶�������", SortIndex = 25, CateId = 2 },
                new BizItems { ItemName = "ҵ��н�ǰ������", SortIndex = 26, CateId = 2 },

                new BizItems { ItemName = "ʳƷ��Ӫ���֤", SortIndex = 1, CateId = 3 },
                new BizItems { ItemName = "���������������֤", SortIndex = 2, CateId = 3 },
                new BizItems { ItemName = "�����Ļ���Ӫ���֤", SortIndex = 3, CateId = 3 },
                new BizItems { ItemName = "�̲�ר���������֤", SortIndex = 4, CateId = 3 },
                new BizItems { ItemName = "ҽ����е��Ӫ���������ࣩ", SortIndex = 5, CateId = 3 },
                new BizItems { ItemName = "ҽ����е��Ӫ���֤�����ࣩ", SortIndex = 6, CateId = 3 },
                new BizItems { ItemName = "��·�������֤", SortIndex = 7, CateId = 3 },
                new BizItems { ItemName = "������ǲ����", SortIndex = 8, CateId = 3 },
                new BizItems { ItemName = "ʩ���ܳа��Ƚ���������", SortIndex = 9, CateId = 3 },

                new BizItems { ItemName = "��ҵ�������", SortIndex = 1, CateId = 4 },
                new BizItems { ItemName = "������ѯ", SortIndex = 2, CateId = 4 },
                new BizItems { ItemName = "˰����ѯ", SortIndex = 3, CateId = 4 },
                new BizItems { ItemName = "�����ѯ", SortIndex = 4, CateId = 4 },
                new BizItems { ItemName = "������ѯ", SortIndex = 5, CateId = 4 },
                new BizItems { ItemName = "˰�ճﻮ", SortIndex = 6, CateId = 4 },
                new BizItems { ItemName = "��ҵ��ѵ����ҵ��ѵ��", SortIndex = 7, CateId = 4 },
                new BizItems { ItemName = "��ҵ��ѵ��������֯�ļ�����ѵ��", SortIndex = 8, CateId = 4 },
                new BizItems { ItemName = "��ҵ�ڲ��������", SortIndex = 9, CateId = 4 },
                new BizItems { ItemName = "�����Ϣ����ѯ��ʵʩ", SortIndex = 10, CateId = 4 },
                new BizItems { ItemName = "���������ϵ����", SortIndex = 11, CateId = 4 },
                new BizItems { ItemName = "��ҵ���", SortIndex = 12, CateId = 4 },
                new BizItems { ItemName = "��ҵս�Թ滮��ѯ", SortIndex = 13, CateId = 4 },
                new BizItems { ItemName = "��ҵ�����������", SortIndex = 14, CateId = 4 },
                new BizItems { ItemName = "��ҵ��ְ����", SortIndex = 15, CateId = 4 },

                new BizItems { ItemName = "����ҵ��", SortIndex = 99, CateId = 5 }
                );

            context.BizSteps.AddOrUpdate(
                new BizSteps { StepName = "ǩ����ͬ����ͷЭ�飩", SortIndex = 1, CateId = 1 },
                new BizSteps { StepName = "�������", SortIndex = 1, CateId = 1 },
                new BizSteps { StepName = "������ȡ", SortIndex = 1, CateId = 1 },
                new BizSteps { StepName = "����", SortIndex = 1, CateId = 1 },
                new BizSteps { StepName = "�����Ƴ��쳣", SortIndex = 1, CateId = 1 },
                new BizSteps { StepName = "���̵���Ǩ��", SortIndex = 1, CateId = 1 },
                new BizSteps { StepName = "�ύ����", SortIndex = 1, CateId = 1 },
                new BizSteps { StepName = "��ȡӪҵִ��", SortIndex = 1, CateId = 1 },
                new BizSteps { StepName = "ӡ�¿���", SortIndex = 1, CateId = 1 },
                new BizSteps { StepName = "��ȡӡ��", SortIndex = 1, CateId = 1 },
                new BizSteps { StepName = "���̲鵵", SortIndex = 1, CateId = 1 },
                new BizSteps { StepName = "˰����Ϣ�ɼ�", SortIndex = 1, CateId = 1 },
                new BizSteps { StepName = "��˰�쳣����", SortIndex = 1, CateId = 1 },
                new BizSteps { StepName = "��˰�쳣����", SortIndex = 1, CateId = 1 },
                new BizSteps { StepName = "��˰����", SortIndex = 1, CateId = 1 },
                new BizSteps { StepName = "��˰����", SortIndex = 1, CateId = 1 },
                new BizSteps { StepName = "���жԹ�����Ϣ���", SortIndex = 1, CateId = 1 },
                new BizSteps { StepName = "����Э��ۿ���ǩ", SortIndex = 1, CateId = 1 },
                new BizSteps { StepName = "��Ʊ��ɼ�˰���ط���", SortIndex = 1, CateId = 1 },
                new BizSteps { StepName = "�������", SortIndex = 1, CateId = 1 },
                new BizSteps { StepName = "���ö���", SortIndex = 1, CateId = 1 },
                new BizSteps { StepName = "������ȡ", SortIndex = 1, CateId = 1 },
                new BizSteps { StepName = "���Ϲ鵵", SortIndex = 1, CateId = 1 },
                new BizSteps { StepName = "���Ϲ黹", SortIndex = 1, CateId = 1 },
                new BizSteps { StepName = "�ͻ��ط�", SortIndex = 1, CateId = 1 },
                new BizSteps { StepName = "�����걨", SortIndex = 1, CateId = 1 },
                new BizSteps { StepName = "��ͨ��¼", SortIndex = 1, CateId = 1 },

                new BizSteps { StepName = "ǩ����ͬ����ͷЭ�飩", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "�������", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "������ȡ", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "���ƾ֤��ȡ", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "ԭʼƾ֤����", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "���ƾ֤¼��", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "������", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "�������", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "ƾ֤��ӡ��װ��", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "�˲���ӡ��װ��", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "�����ӡ��װ��", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "��˰�걨", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "���ɹ�˰˰��", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "��˰�걨", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "���ɵ�˰˰��", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "��ҵ����˰�������", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "��˰��˰�걨���ӡ��װ��", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "��˰��˰�걨���ӡ��װ��", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "���ױ���", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "������˰�걨����ӵ��浵", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "�������", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "˰�����칺������", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "��Ʊ����", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "��Ʊ���������ύ", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "��Ʊ���������������Ȳ�ѯ", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "��Ʊ�����������", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "��Ʊ��λ�����ύ", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "��Ʊ��λ�����������Ȳ�ѯ", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "��Ʊ��λ�������", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "�Թ��������������������ύ", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "�Թ�����������������������", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "�Թ����������֤��ȡ��������", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "�Թ���������ͨ", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "��˰��ӡ����Э��ۿ�", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "���д�ӡ����Э��ۿ�", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "����ǩ������Э��ۿ�", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "����Э��ۿ�ع�˰", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "����Э��ۿ�ص�˰", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "�м��˾�ҵ���Ͻ�˶�������", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "��Ʊ����", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "˰�����ֳ��忨�����", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "һ����˰���϶������ύ", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "���ö���", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "������ȡ", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "��ͨ��¼", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "�ͷ��ط�", SortIndex = 1, CateId = 2 },
                new BizSteps { StepName = "���Ϲ黹", SortIndex = 1, CateId = 2 },

                new BizSteps { StepName = "ǩ����ͬ����ͷЭ�飩", SortIndex = 1, CateId = 3 },
                new BizSteps { StepName = "�������", SortIndex = 1, CateId = 3 },
                new BizSteps { StepName = "������ȡ", SortIndex = 1, CateId = 3 },
                new BizSteps { StepName = "�������׼��", SortIndex = 1, CateId = 3 },
                new BizSteps { StepName = "��������ύ", SortIndex = 1, CateId = 3 },
                new BizSteps { StepName = "��������������Ȳ�ѯ", SortIndex = 1, CateId = 3 },
                new BizSteps { StepName = "�����漰���ý���", SortIndex = 1, CateId = 3 },
                new BizSteps { StepName = "����Ҫ��", SortIndex = 1, CateId = 3 },
                new BizSteps { StepName = "���Ĳ����ύ", SortIndex = 1, CateId = 3 },
                new BizSteps { StepName = "��ȡ֤��", SortIndex = 1, CateId = 3 },
                new BizSteps { StepName = "���ö���", SortIndex = 1, CateId = 3 },
                new BizSteps { StepName = "��ͨ��¼", SortIndex = 1, CateId = 3 },
                new BizSteps { StepName = "������ȡ", SortIndex = 1, CateId = 3 },
                new BizSteps { StepName = "���Ϲ黹", SortIndex = 1, CateId = 3 },
                new BizSteps { StepName = "�ͻ��ط�", SortIndex = 1, CateId = 3 },

                new BizSteps { StepName = "ǩ����ͬ����ͷЭ�飩", SortIndex = 1, CateId = 4 },
                new BizSteps { StepName = "�������", SortIndex = 1, CateId = 4 },
                new BizSteps { StepName = "������ȡ", SortIndex = 1, CateId = 4 },
                new BizSteps { StepName = "��ͨ��¼", SortIndex = 1, CateId = 4 },
                new BizSteps { StepName = "����ύ", SortIndex = 1, CateId = 4 },
                new BizSteps { StepName = "����������޶�", SortIndex = 1, CateId = 4 },
                new BizSteps { StepName = "���հ汨���ύ", SortIndex = 1, CateId = 4 },
                new BizSteps { StepName = "���ö���", SortIndex = 1, CateId = 4 },
                new BizSteps { StepName = "������ȡ", SortIndex = 1, CateId = 4 },
                new BizSteps { StepName = "���Ϲ黹", SortIndex = 1, CateId = 4 },
                new BizSteps { StepName = "�ͻ��ط�", SortIndex = 1, CateId = 4 },

                new BizSteps { StepName = "ǩ����ͬ����ͷЭ�飩", SortIndex = 1, CateId = 5 },
                new BizSteps { StepName = "�������", SortIndex = 1, CateId = 5 },
                new BizSteps { StepName = "������ȡ", SortIndex = 1, CateId = 5 },
                new BizSteps { StepName = "��ͨ��¼", SortIndex = 1, CateId = 5 },
                new BizSteps { StepName = "����ύ", SortIndex = 1, CateId = 5 },
                new BizSteps { StepName = "����������޶�", SortIndex = 1, CateId = 5 },
                new BizSteps { StepName = "���հ汨���ύ", SortIndex = 1, CateId = 5 },
                new BizSteps { StepName = "���ö���", SortIndex = 1, CateId = 5 },
                new BizSteps { StepName = "������ȡ", SortIndex = 1, CateId = 5 },
                new BizSteps { StepName = "���Ϲ黹", SortIndex = 1, CateId = 5 },
                new BizSteps { StepName = "�ͻ��ط�", SortIndex = 1, CateId = 5 }

                );
        }
    }
}
